var answer = [];

function onsub() {
    answer = [];
    var max = document.querySelectorAll('.question').length;
    debugger;
    if (max == 0) {
        return false;
    }

    for (let j = 0; j < max; j++) {
        var answers = document.querySelectorAll('.question' + (j + 1));

        for (let i = 0; i < answers.length; i++) {
            const item = answers[i];

            if (item.checked) {
                answer.push(parseInt(item.value));
                break;
            }

        }

        if (answer[j] == null) {
            answer.push(0);
        }

    }

    document.getElementById('answers').value = answer.toString();

    console.log(answer);

    return true;
}