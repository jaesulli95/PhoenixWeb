$('tr.project-task-row').on('click', function (e) {
    var $name = $(this).find("td:nth-child(1)").text();
    var $description = $(this).find("td:nth-child(2)").text();
    var $type = $(this).find("td:nth-child(3)").text();
    var $timestamp = $(this).find("td:nth-child(4)").text();

});


function SetChecked(ElementIdName, status) {
    if (status == "True") {
        document.getElementById(ElementIdName).checked = true;
        return;
    } else if (status == "False") {
        document.getElementById(ElementIdName).checked = false;
    }
}

$('tr.entry-row').on('click', function (e) {

    var $JournalData = document.getElementById("JournalData");
    if ($JournalData.style.visibility == 'hidden') {
        $JournalData.style.visibility = 'visible';
    }

    //Daily Stats
    var $date = $(this).find("td:nth-child(2)").text();
    var $water = $(this).find("td:nth-child(6)").text();
    var $protein = $(this).find("td:nth-child(5)").text();
    var $calories = $(this).find("td:nth-child(7)").text();
    var $weight = $(this).find("td:nth-child(8)").text();


    document.getElementById("dvWater").value = $water;
    document.getElementById("dvProtein").value = $protein;
    document.getElementById("dvCalories").value = $calories;
    document.getElementById("dvWeight").value = $weight;
    document.getElementById("dvJournalEntryDate").innerHTML = $date;

    //FOOD
    var $food = $(this).find("td:nth-child(9)").text();
    document.getElementById("dvFood").value = $food;

    //Checked
    var $gym = $(this).find("td:nth-child(4)").text();
    var $cooked = $(this).find("td:nth-child(3)").text();
    var $mochas = $(this).find("td:nth-child(10)").text();
    var $erotica = $(this).find("td:nth-child(11)").text();
    var $reddit = $(this).find("td:nth-child(12)").text();

    SetChecked("dvGymExercise", $gym);
    SetChecked("dvCookedFood", $cooked);
    SetChecked("dvNoMochas", $mochas);
    SetChecked("dvNoErotica", $erotica);
    SetChecked("dvNoReddit", $reddit);
});