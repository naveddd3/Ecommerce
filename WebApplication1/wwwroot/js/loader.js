
const preloader = {
    load: function () {
        const loader = document.getElementById('loader');
        loader.style.display = 'flex';
        //document.body.style.overflow = 'hidden';
    },
    remove: function () {
        const loader = document.getElementById('loader');
        loader.style.display = 'none';
        //document.body.style.overflow = 'auto';
    }
};


