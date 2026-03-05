(function() {
    var modal = document.getElementById('search-modal');
    var backdrop = document.getElementById('search-backdrop');
    var input = document.getElementById('search-input');
    var listEl = document.getElementById('search-list');
    var emptyEl = document.getElementById('search-empty');
    var loadingEl = document.getElementById('search-loading');
    var noResultsEl = document.getElementById('search-no-results');
    var countEl = document.getElementById('search-count');
    var selectedIndex = -1;
    var currentResults = [];
    var debounceTimer;
    var abortController;

    var icons = {
        page: '<svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="2"><path stroke-linecap="round" stroke-linejoin="round" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z"/></svg>',
        news: '<svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="2"><path stroke-linecap="round" stroke-linejoin="round" d="M19 20H5a2 2 0 01-2-2V6a2 2 0 012-2h10a2 2 0 012 2v1m2 13a2 2 0 01-2-2V7m2 13a2 2 0 002-2V9a2 2 0 00-2-2h-2m-4-3H9M7 16h6M7 8h6v4H7V8z"/></svg>',
        event: '<svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="2"><path stroke-linecap="round" stroke-linejoin="round" d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z"/></svg>',
        content: '<svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="2"><path stroke-linecap="round" stroke-linejoin="round" d="M4 6h16M4 12h16M4 18h7"/></svg>'
    };

    function open() {
        modal.classList.remove('hidden');
        document.body.style.overflow = 'hidden';
        input.value = '';
        selectedIndex = -1;
        currentResults = [];
        showState('empty');
        requestAnimationFrame(function() { input.focus(); });
        if (window.lucide) lucide.createIcons({ nodes: [modal] });
    }

    function close() {
        modal.classList.add('hidden');
        document.body.style.overflow = '';
        if (abortController) abortController.abort();
    }

    function showState(state) {
        emptyEl.classList.toggle('hidden', state !== 'empty');
        loadingEl.classList.toggle('hidden', state !== 'loading');
        listEl.classList.toggle('hidden', state !== 'results');
        noResultsEl.classList.toggle('hidden', state !== 'none');
        countEl.classList.toggle('hidden', state !== 'results');
    }

    function doSearch(query) {
        if (!query.trim()) {
            showState('empty');
            currentResults = [];
            return;
        }

        if (abortController) abortController.abort();
        abortController = new AbortController();

        showState('loading');

        fetch('/api/search?q=' + encodeURIComponent(query.trim()), { signal: abortController.signal })
            .then(function(res) { return res.json(); })
            .then(function(results) {
                currentResults = results;
                if (results.length === 0) {
                    showState('none');
                    return;
                }
                showState('results');
                countEl.textContent = results.length + ' result' + (results.length !== 1 ? 's' : '');
                renderResults(results);
            })
            .catch(function(err) {
                if (err.name !== 'AbortError') {
                    showState('none');
                }
            });
    }

    function renderResults(results) {
        var html = '';
        results.forEach(function(item, i) {
            var icon = icons[item.type] || icons.page;
            html += '<a href="' + item.url + '" ' +
                'class="search-result flex items-center gap-3 px-4 py-3 rounded-lg cursor-pointer transition-colors ' +
                (i === selectedIndex ? 'bg-foreground/5' : 'hover:bg-foreground/5') + '" ' +
                'data-index="' + i + '">' +
                '<span class="text-foreground/40 flex-shrink-0">' + icon + '</span>' +
                '<div class="flex-1 min-w-0">' +
                '<div class="text-sm font-medium text-foreground truncate">' + item.title + '</div>' +
                (item.description ? '<div class="text-xs text-foreground/50 truncate">' + item.description.replace(/<[^>]*>/g, '') + '</div>' : '') +
                '</div>' +
                '<span class="text-[10px] uppercase tracking-wider font-bold text-foreground/25 bg-foreground/5 px-1.5 py-0.5 rounded flex-shrink-0">' + item.type + '</span>' +
                '</a>';
        });
        listEl.innerHTML = html;
        selectedIndex = -1;
    }

    function updateSelection() {
        var items = listEl.querySelectorAll('.search-result');
        items.forEach(function(el, i) {
            if (i === selectedIndex) {
                el.classList.add('bg-foreground/5');
                el.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
            } else {
                el.classList.remove('bg-foreground/5');
            }
        });
    }

    document.addEventListener('keydown', function(e) {
        if ((e.metaKey || e.ctrlKey) && e.key === 'k') {
            e.preventDefault();
            modal.classList.contains('hidden') ? open() : close();
            return;
        }
        if (e.key === '/' && modal.classList.contains('hidden')) {
            var tag = document.activeElement.tagName;
            if (tag !== 'INPUT' && tag !== 'TEXTAREA' && !document.activeElement.isContentEditable) {
                e.preventDefault();
                open();
            }
        }
    });

    input.addEventListener('keydown', function(e) {
        if (e.key === 'Escape') { close(); return; }
        if (e.key === 'ArrowDown') {
            e.preventDefault();
            if (currentResults.length > 0) {
                selectedIndex = Math.min(selectedIndex + 1, currentResults.length - 1);
                updateSelection();
            }
            return;
        }
        if (e.key === 'ArrowUp') {
            e.preventDefault();
            if (currentResults.length > 0) {
                selectedIndex = Math.max(selectedIndex - 1, 0);
                updateSelection();
            }
            return;
        }
        if (e.key === 'Enter') {
            e.preventDefault();
            if (selectedIndex >= 0 && selectedIndex < currentResults.length) {
                window.location.href = currentResults[selectedIndex].url;
            } else if (currentResults.length > 0) {
                window.location.href = currentResults[0].url;
            }
            return;
        }
    });

    input.addEventListener('input', function() {
        clearTimeout(debounceTimer);
        debounceTimer = setTimeout(function() { doSearch(input.value); }, 300);
    });

    backdrop.addEventListener('click', close);
    window.openSearch = open;
})();
