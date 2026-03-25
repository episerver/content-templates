// Header scroll effect & mobile menu
(function() {
    var header = document.getElementById('site-header');
    var toggle = document.getElementById('mobile-menu-toggle');
    var menu = document.getElementById('mobile-menu');
    var overlay = document.getElementById('mobile-menu-overlay');
    var closeBtn = document.getElementById('mobile-menu-close');

    if (header) {
        window.addEventListener('scroll', function() {
            if (window.scrollY > 200) {
                header.classList.add('bg-white/90', 'backdrop-blur');
                header.classList.remove('bg-transparent');
            } else {
                header.classList.remove('bg-white/90', 'backdrop-blur');
                header.classList.add('bg-transparent');
            }
        });
    }

    function openMenu() {
        menu.classList.remove('translate-x-full');
        overlay.classList.remove('opacity-0', 'pointer-events-none');
        document.body.style.overflow = 'hidden';
        if (window.lucide) lucide.createIcons();
    }

    function closeMenu() {
        menu.classList.add('translate-x-full');
        overlay.classList.add('opacity-0', 'pointer-events-none');
        document.body.style.overflow = '';
    }

    if (toggle) toggle.addEventListener('click', openMenu);
    if (closeBtn) closeBtn.addEventListener('click', closeMenu);
    if (overlay) overlay.addEventListener('click', closeMenu);

    if (menu) {
        menu.querySelectorAll('.mobile-nav-link').forEach(function(link) {
            link.addEventListener('click', closeMenu);
        });
    }
})();

var isEditMode = window.epi != null || window.frameElement != null;

lucide.createIcons();

if (isEditMode) {
    document.querySelectorAll('.scroll-fade-up').forEach(function(el) {
        el.classList.add('visible');
    });

    var lucideTimer;
    new MutationObserver(function(mutations) {
        var dominated = mutations.every(function(m) {
            return m.target.closest('[data-lucide]') || m.target.tagName === 'svg';
        });
        if (dominated) return;
        clearTimeout(lucideTimer);
        lucideTimer = setTimeout(function() { lucide.createIcons(); }, 200);
    }).observe(document.body, { childList: true, subtree: true });
} else {
    var observer = new IntersectionObserver(function(entries) {
        entries.forEach(function(entry) {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible');
                observer.unobserve(entry.target);
            }
        });
    }, { threshold: 0.15 });
    document.querySelectorAll('.scroll-fade-up').forEach(function(el) { observer.observe(el); });
}

(function() {
    var parallaxSpeeds = { 'parallax-low': '0.95', 'parallax-medium': '0.9', 'parallax-high': '0.8' };
    Object.keys(parallaxSpeeds).forEach(function(cls) {
        document.querySelectorAll('.' + cls).forEach(function(el) {
            el.setAttribute('data-parallax-speed', parallaxSpeeds[cls]);
        });
    });

    var parallaxEls = document.querySelectorAll('[data-parallax-speed]');
    if (!parallaxEls.length) return;
    var ticking = false;

    function update() {
        if (window.innerWidth < 768) {
            parallaxEls.forEach(function(el) { el.style.transform = ''; });
            ticking = false;
            return;
        }
        var scrollY = window.pageYOffset || 0;
        parallaxEls.forEach(function(el) {
            var speed = parseFloat(el.getAttribute('data-parallax-speed')) || 1;
            var offset = scrollY * (speed - 1);
            el.style.transform = 'translate3d(0,' + offset.toFixed(2) + 'px,0)';
        });
        ticking = false;
    }

    function onScroll() {
        if (!ticking) { requestAnimationFrame(update); ticking = true; }
    }

    window.addEventListener('scroll', onScroll, { passive: true });
    update();
})();
