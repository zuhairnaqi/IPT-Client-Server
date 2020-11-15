function onSubjectChange(event) {
    const subjectNum = +event.value;
    if (subjectNum < 0 || subjectNum > 15) {
        event.value = '0';
        return alert('Subjects number should be within 0 and 15');
    }
    let entries = '';
    for (let i = 0; i < subjectNum; i++) {
        entries += `
        <tr class="text-center">
            <td>
                <div class="col">
                    <input type="text" class="form-control subjectName" placeholder="Subject Name">
                </div>
            </td>
            <td>
                <div class="col">
                    <input type="number" class="form-control marksObtain" placeholder="Marks Obtained"  onkeyup="checkSubjectNumber(this)">
                </div>
            </td>
        </tr>`;
    }
    $('#tbody').html(entries);
}

function checkSubjectNumber(event) {
    const subjectNum = +event.value;
    if (subjectNum < 0 || subjectNum > 100) {
        event.value = '0';
        return alert('Obtained marks should be within 0 and 100');
    }
}