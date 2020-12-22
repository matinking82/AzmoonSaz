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

function deleteClass(classId) {
    debugger;
    var modalcontent = document.getElementById('modal-content');
    modalcontent.innerHTML = '';


    var p = document.createElement('p');
    p.innerHTML = "آیا از حذف این کلاس اطمینان دارید؟";

    var div = document.createElement('div');
    div.classList.add('alert', 'alert-warning');
    div.innerHTML = "تمام دانش آموزان و آزمون های این کلاس حذف خواهند شد";

    var a = document.createElement('a');
    a.href = "classes/delete/" + classId;
    a.classList.add('btn', 'btn-danger');
    a.innerHTML = "حذف";

    modalcontent.appendChild(p);
    modalcontent.appendChild(div);
    modalcontent.appendChild(a);

    ShowModal();
}


async function ShowModal() {
    $("#main-modal").modal('show');
}

function editClass(classId) {
    $('#modal-content').load('/Classes/Edit/' + classId);

    ShowModal();
}

function createStudent(classId) {
    $('#modal-content').load('/Students/Create/' + classId);
    ShowModal();
}