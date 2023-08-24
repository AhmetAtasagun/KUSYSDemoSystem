/* ============ Student Page Scripts ============ */
var studentId = document.querySelector("input#studentId");
var studentFirstName = document.querySelector("input#studentFirstName");
var studentLastName = document.querySelector("input#studentLastName");
var studentBirthDate = document.querySelector("input#studentBirthDate");
var modalDom = document.querySelector("#studentEditModal");
var modalForm = modalDom.querySelector("form");

// Detail Open
document.querySelectorAll("[data-bs-target='#studentDetailModal']").forEach((button) => {
    button.addEventListener("click", async () => {
        var id = button.attributes['data-itemid'].value;
        await GetCourses(document.querySelector("#studentDetailModal .courses"));
        await SelectedCourses(id, document.querySelector("#studentDetailModal .courses"));
        var studentDetail = await RequestGet(`/Student/Get?id=${id}`);
        document.querySelector("span#studentId").textContent = studentDetail.id;
        document.querySelector("span#studentFirstName").textContent = studentDetail.firstName;
        document.querySelector("span#studentLastName").textContent = studentDetail.lastName;
        document.querySelector("span#studentBirthDate").textContent = studentDetail.birthDate;
    });
});

// Add Open
document.querySelector("#studentAdd").addEventListener("click", async () => {
    Clear();
    await GetCourses(document.querySelector("#studentEditModal .courses"));
    modalForm.attributes["action"].value = "/Student/Add";
    document.querySelector("#studentEditModalLabel").innerText = "Add";
    var modal = new bootstrap.Modal(modalDom);
    modal.show();
});

// Update Open
document.querySelectorAll("[data-bs-target='#studentEditModal']").forEach((button) => {
    button.addEventListener("click", async () => {
        Clear();
        modalForm.attributes["action"].value = "/Student/Update";
        document.querySelector("#studentEditModalLabel").innerText = "Edit";

        var id = button.attributes['data-itemid'].value;
        await GetCourses(document.querySelector("#studentEditModal .courses"));
        await SelectedCourses(id, document.querySelector("#studentEditModal .courses"));
        var studentDetail = await RequestGet(`/Student/Get?id=${id}`);
        studentId.value = studentDetail.id;
        studentFirstName.value = studentDetail.firstName;
        studentLastName.value = studentDetail.lastName;
        studentBirthDate.value = new Date(studentDetail.birthDate).toISOString().split('T')[0];
    });
});

// Delete Dialog
document.querySelectorAll(".studentRemove").forEach((button) => {
    button.addEventListener("click", async () => {
        var deleteModalDom = document.querySelector("#studentDeleteModal");
        var deleteModal = new bootstrap.Modal(deleteModalDom);
        deleteModal.show();
        deleteModalDom.querySelector("#deleteStudentId").value = button.attributes['data-itemid'].value;
    })
});

function Clear() {
    studentId.value = "";
    studentFirstName.value = "";
    studentLastName.value = "";
    studentBirthDate.value = "";
}

async function GetCourses(courseSelect) {
    while (courseSelect.childNodes[0] != null) {
        courseSelect.childNodes[0].remove();
    }
    var courses = await RequestGet(`/Course/GetList?id=${0}`);
    var options = courses.map(m => {
        let opt = document.createElement("option");
        opt.value = m.id;
        opt.innerText = m.courseName;
        return opt;
    });
    // `<option value=${m.id}>${m.courseName}</option>`);
    for (var option of options) {
        courseSelect.appendChild(option);
    }
}
async function SelectedCourses(id, courseSelect) {
    var courses = await RequestGet(`/Course/GetList?id=${id}`);

    for (var option of courseSelect.childNodes) {
        for (var course of courses) {
            if (option.value == course.id)
                option.setAttribute("selected", true);
        }
    }

    //var nodes = courseSelect.childNodes;
    //for (var course of courses) {
    //    var node = Array.from(nodes).filter(f => f.attributes["value"].value == course.id);
    //    if (node != null)
    //        node.setAttribute("selected", true);
    //}
}