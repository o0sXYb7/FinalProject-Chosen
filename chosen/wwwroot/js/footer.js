function adjustFooter() {
    const body = document.body;
    const html = document.documentElement;
    const footer = document.querySelector('footer');

    const height = Math.max(body.scrollHeight, body.offsetHeight, html.clientHeight, html.scrollHeight, html.offsetHeight);

    if (height > window.innerHeight) {
        footer.style.position = 'relative';
    } else {
        footer.style.position = 'fixed';
        footer.style.bottom = '0';
        footer.style.left = '0';
        footer.style.right = '0';
    }
}

window.addEventListener('resize', adjustFooter);
document.addEventListener('DOMContentLoaded', adjustFooter);
