start()

function start() {

    var menuItems = document.querySelectorAll('.menu-item div.m-main');

    for (let i = 0; i < menuItems.length; i++) {
        const menuItem = menuItems[i];

        menuItem.addEventListener('mouseover', function() {
            menuItemOver(menuItem);
        });

        menuItem.addEventListener('mouseout', function() {
            menuItemOut(menuItem);
        });
    }

}

async function menuItemOver(menuItem) {
    menuItem.children[0].style = "width:600px;height:600px;top:-100px;left:-100px;";
}

async function menuItemOut(menuItem) {
    menuItem.children[0].removeAttribute('style');
}