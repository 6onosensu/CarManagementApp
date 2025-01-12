
const addRecord = document.getElementById('addRecord');
addRecord.addEventListener('click', function () {
    const container = document.getElementById('addRecordContainer');
    const currentDisplay = window.getComputedStyle(container).display;
    if (currentDisplay === 'block') {
        container.style.display = 'none';
    } else {
        container.style.display = 'block';
    }
});

const updateRecord = document.querySelectorAll('.updateRecordButton');
const updateContainer = document.getElementById('updateRecordContainer');
const updateForm = updateContainer.querySelector('form');
updateRecord.forEach(button => {
    button.addEventListener('click', function () {
        const сarId = this.getAttribute('data-car-id');
        const recordId = this.getAttribute('data-record-id');
        const title = this.getAttribute('data-title');
        const description = this.getAttribute('data-description');

        updateForm.querySelector('input[name="CarId"]').value = сarId;
        updateForm.querySelector('input[name="Id"]').value = recordId;
        updateForm.querySelector('input[name="Title"]').value = title;
        updateForm.querySelector('textarea[name="Description"]').value = description;

        updateContainer.style.display = 'block';
    });
});