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

function calculate() {
    const studentName = $('#studentName').val();
    const subjectNum = $('#subjectNum').val();
    let subjectsDetail = [];
    const subjectsName = document.getElementsByClassName('subjectName');
    const marksObtain = document.getElementsByClassName('marksObtain');
    for (let i = 0; i < subjectsName.length; i++) {
        subjectsDetail.push({
            name: subjectsName[i].value,
            marks: +marksObtain[i].value,
        })
    }
    const requestData = {
        studentName,
        subjectNum,
        subjectsDetail
    };
    $.ajax({
        method: 'GET',
        url: 'http://localhost:60178/WebService1.asmx/calculatePercentage',
        contentType: 'application/json',
        data: { request: JSON.stringify(requestData) },
        success: function(result) {
            const data = JSON.parse(result.d);
            const { maxSubject, minSubject, percentage } = data;
            $('#minMarksSubject').val(minSubject.name);
            $('#minMarksNum').val(minSubject.marks);
            $('#maxMarksSubject').val(maxSubject.name);
            $('#maxMarksNum').val(maxSubject.marks);
            $('#percentage').val(percentage);
        }
    });

}