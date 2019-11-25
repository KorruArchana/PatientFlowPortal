var tab = " <div class='mdl-layout__tab-bar mdl-js-ripple-effect'>\
      <a href='#QuestionsContent' class='mdl-layout__tab is-active' id='questionsTab' style='border-bottom: 2px solid #006eb6; color: #006eb6;'>Questions</a>\
      <a href='#SettingsContent' class='mdl-layout__tab' id='settingsTab'>Settings</a>\
    </div>";

var counter = 0;
var questionId = 0;
var quesId = 0;
var optionsList = [];
var listOfQuestions = [];
var questionOrder = 0;
var noVisit = 0;
var orderArray = [];
var questionnnaireId = 0;
var deletedQuestion = [];
var questionDivId;
var questionClonedDiv;
var previous = 0;
var deleteCardId = 0;
var EditQuesId = 0;
var codeVisibility = false;
var nestedQuestionVisibility,TargetQuestionVisibility = false;
var codeValid = true;
var Question = {};
var nestedQuestions = {};
var nestedQuestionList = [];
var questionList;

function TargetDemographicIntialize() {
    $('#TargetGender').drop("set selected", 1);
    $('#TargetAge').drop("set selected", 1);
    $('#TargetAge1,#TargetAge2').drop("set selected", 50);
    $('#TargetAge1,#TargetAge2').prop("selectedIndex", 50);
}

function ShowingMultiplchoicequestion() {
    $('#TextAnswerDiv,#NumericTextAnswerdiv').css({ 'display': 'none' });
    $('#MultipleChoiceOptions,#optionFields').css({ 'display': 'block' });
    $('#optionCharLimit').val(0);
}

function CodeValidation(CodeType, codeData) {
    if (CodeType === "readCode") {
        if (codeData.length >= 2 && codeData.length <= 12) {
            codeValid = true;
        }
        else if (codeData!=""){
            $('#CodeErrroMessage').text("Minimum Characters of read code is 2 and Maximum Characters is 12");
            $('#CodeErrroMessage').css({ 'display': 'block' });
            codeValid = false;
        }
    }
    else if (CodeType === "snomedCode" && codeData!="") {
        var isDigit = /^\d+$/.test(codeData);
        var isStartingZero = /^0[0-9].*$/.test(codeData);
        if (parseInt(codeData) < 0) {
            $('#CodeErrroMessage').text("Snomed Code cannot be negative");
            $('#CodeErrroMessage').css({ 'display': 'block' });
            codeValid = false;
        }
        else if (!isDigit) {
            $('#CodeErrroMessage').text("Snomed code can be only numeric");
            $('#CodeErrroMessage').css({ 'display': 'block' });
            codeValid = false;
        }
        else if (isStartingZero) {
            $('#CodeErrroMessage').text("Snomed code should not start with zero");
            $('#CodeErrroMessage').css({ 'display': 'block' });
            codeValid = false;
        }
        else if (codeData.toString().length >= 6 && codeData.toString().length <= 18) {
            codeValid = true;
        }
        else if (codeData.toString().length <= 5) {
            $('#CodeErrroMessage').text("Minimum Characters of snomed code is 6 and Maximum Characters is 18");
            $('#CodeErrroMessage').css({ 'display': 'block' });
            codeValid = false;
        }
    }
}

function SetTabVisibility(ele) {
    hideLoading();
    if (!($(ele)).is(':visible')) {
        $('#questionsTab,#settingsTab').toggleClass("is-active");
        $('#SettingsContent,#QuestionsContent').toggle();
    }
    if ($('#settingsTab').hasClass('is-active')) {
        $('#AddQuestion1').hide();
        $('#AddQuestion2').hide();
    }
    else {
        if ($('.mdl-layout:not(.is-small-screen)').length > 0) {
            $('#AddQuestion1').show();
        }
        else {
            $('#AddQuestion2').show();
        }
    }
}

function ChangeListofQuestionforDelete(deleteId, deleteCardId) {
    $(listOfQuestions).each(function (i, v) {
        if (v.order >= deleteId && v.quesId !== deleteCardId) {
            var id = v.order - 1;
            $("#question" + v.order).attr("data-order", id);
            $("#question" + v.order).attr("data-id", id);
            v.order = id;
            v.quesId = id;
        }
    });
}

function ChangeListofQuestion(previous) {
    $(listOfQuestions).each(function (i, v) {
        if (v.order >= previous) {
            var id = v.order + 1;
            $("#question" + v.order).attr("data-order", id);
            $("#question" + v.order).attr("data-id", id);
            v.order = id;
            v.quesId = id;
        }
    });
}

function setDefaultforQuestions() {
    $('#QuestionCard').css({ 'display': 'none' });
    $('#txt_question').val('');
    $('#optionListTable tr').remove();
    $('#txt_options').attr('placeholder', 'Option 1');
    $('#MenuDrop').drop("set selected", 4);
}

function makeDraggable() {
    $(this).css('position', 'initial');
    $(this).css('opacity', '');
    $('.questionDiv').draggable({
        containment: 'parent',
        cursor: 'move',
        revert: 'invalid',
        opacity: 0.3,
        start: function (event, ui) {
            window.startPos = $(this).offset();
        }
    });
}

function makeDropable() {
    $(this).css('position', 'initial');
    $(this).css('opacity', '');
    $('.questionDiv').droppable({
        drop: function (event, ui) {
            orderArray = [];
            var $from = $(ui.draggable),
                $fromParent = $from.parent(),
                $to = $(this).children(),
                $toParent = $(this);

            window.endPos = $to.offset();

            var FromPositionDiv = $from;
            var order1 = FromPositionDiv.attr("data-order");
            var FromPositionClonedDiv = $from.clone();
            var ToPositionDiv = $toParent;
            var order2 = ToPositionDiv.attr("data-order");
            var ToPositionClonedDiv = $toParent.clone();

            FromPositionDiv.replaceWith(ToPositionClonedDiv).css('position', 'absolute');
            ToPositionDiv.replaceWith(FromPositionClonedDiv).css('position', 'absolute');

            $(listOfQuestions).each(function (i, v) {
                if (v.order == order1) {
                    v.order = order2;
                }
                else if (v.order == order2) {
                    v.order = order1;
                }
            });
            $('.AddedQuestionDiv').trigger('newQuestionAdded');
        }
    });
}

function checkifEmiswebOrg(orgid) {
    $.ajax({
        url: "../Questionnaire/IsEmisWebOrganisation",
        datatype: "JSON",
        data: "OrganisationId=" + orgid,
        type: "Get",
        success: function (data) {
            if (data.success) {
                $('#SnomedCodeDiv').css({ 'display': 'block' });
            }
            else {
                $('#SnomedCodeDiv').css({ 'display': 'none' });
            }
        },
        error: function (ex) {
            //alert('Failed to get departments under' + $('#orgdropdown').text() + 'organisation. ' + ex);
        }
    });
}

function getkiosks(orgid) {
    showLoading();
    $.ajax({
        url: '/Kiosk/GetKioskList',
        datatype: 'JSON',
        type: "Get",
        data: 'organisationId=' + orgid,
        success: function (data) {
            hideLoading();
            $("#kioskDropdown").empty().drop("clear");
            $.each(data, function (i, kiosk) {
                $("#kioskDropdown").append('<option value="' + kiosk.Id + '">' +
                    kiosk.KioskName + '</option>');
            });
        }
    });
}

function GetstringList(intList) {
    var stringList = [];
    if (intList !== null) {
        $.each(intList, function (i, Id) { stringList.push(Id.toString()); });
    }
    return stringList;
}

function EditCardIntialize(el) {
    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    $('#txtOption').css({ 'display': 'none' });
    $('#CodeErrroMessage').css({ 'display': 'block' });
    $('#CodeErrroMessage').text('');
    $('#OptionTextDiv').css({ 'width': '55%' });
    questionDivId = $(el).attr("id");
    $('#questionOptionErrorMessage,#questionErrorMessage').css({ 'visibility': 'hidden' });
    var questionId = $(el).attr('data-id');
    var questionOrder = $(el).attr('data-order');
    var questiontext = $(el).attr('data-questionText');
    var dropdownValue = $(el).attr('data-action');
    var Gender = $(el).attr('data-gender');
    var operation = $(el).attr('data-operation');
    var Age1 = $(el).attr('data-Age1');
    var Age2 = $(el).attr('data-Age2');
    var qId = $(el).attr('data-questionId');

    var Anonymous = $('input[type=radio][name="Isanonymous"]:checked').val();
    if ((!TargetQuestionVisibility) || Anonymous === "true" || (Gender === "1" && operation === "1") ) {
        $('#TargetDemographic').css({ 'display': 'none' });
        $('#checkIconforTarget').css({ 'display': 'none' });
        $('#TargetDemographicsMenu').addClass('QuestionActionMenuAction');
        if (Anonymous === "true") {
            $('#checkIconforCode').css({ 'display': 'none' });
            $('#QuestionAction').find("#MapClinicalCodes,#TargetDemo").addClass("disabled");
            $('#CodeText,#CodesDiv').css({ 'display': 'none' });
        }
    }
    else {
        $('#TargetDemographic').css({ 'display': 'block' });

        if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))  // If Internet Explorer, return version number
        {
            $('#checkIconforTarget').css({ 'display': '-ms-inline-grid' });
        }
        else {
            $('#checkIconforTarget').css({ 'display': '-webkit-inline-box' });
        }

        $('#TargetDemographicsMenu').removeClass('QuestionActionMenuAction');
        $('#TargetGender').drop("set selected", Gender);
        $('#TargetAge').drop("set selected", operation);
        $('#TargetAge1').drop("set selected", Age1);
        $('#TargetAge2').css({ 'display': 'none' });
        if (Age2 > 0) {
            $('#TargetAge2').css({ 'display': 'block' });
            $('#TargetAge2').drop("set selected", Age2);
        }
    }
    $(el).css({ 'display': 'none' });
    $(el).next().append($("#QuestionCard")).css({ 'display': 'block' });
    $('#QuestionCard').css({ 'display': 'block', 'border-left': '4px solid #d3772b' });
    $("#txt_question").val(questiontext);
    $('#MenuDrop').drop("set selected", dropdownValue);
    $('#QuestionId').val(questionId);

    $('#deleteCard').attr("data-id", questionId);
    $('#deleteCard').attr("data-order", questionOrder);

    if (dropdownValue === "4") {
        var tablerow = "";
        var options = $(el).attr('data-optionList');
        var questionoptionslist = JSON.parse(decodeURIComponent(options));
        for (var i = 0; i < questionoptionslist.length; i++) {
            optionsList.push(questionoptionslist[i]);
            var codeType = $('input[type=radio][name="Code"]:checked').val();
            if (codeType === "readCode" && questionoptionslist[i].QuestionOptionCode !== "") {
                tablerow = FormQuestionOptionTable(questionoptionslist[i].QuestionOption, codeVisibility, codeType, questionoptionslist[i].QuestionOptionCode, questionoptionslist[i].OptionId, true, nestedQuestionVisibility, qId);
                $('#optionListTable >tbody').append(tablerow);
                SetOptionsWidth();
            }
            else if (codeType === "snomedCode" && questionoptionslist[i].QuestionSnomedOptionCode >= 0) {
                tablerow = FormQuestionOptionTable(questionoptionslist[i].QuestionOption, codeVisibility, codeType, questionoptionslist[i].QuestionSnomedOptionCode, questionoptionslist[i].OptionId, true, nestedQuestionVisibility, qId);
                $('#optionListTable >tbody').append(tablerow);
                SetOptionsWidth();
            }
            else {
                tablerow = FormQuestionOptionTable(questionoptionslist[i].QuestionOption, codeVisibility, codeType, "", questionoptionslist[i].OptionId, true, nestedQuestionVisibility, qId);
                $('#optionListTable >tbody').append(tablerow);
            }
            SetOptionsWidth();
            if (questionoptionslist[i].NestedQuestionId === -1) {
                questionoptionslist[i].NestedQuestionId = 2;
            }
            else if (questionoptionslist[i].NestedQuestionId === 0) {
                questionoptionslist[i].NestedQuestionId = 1;
            }
            if (nestedQuestionVisibility) {
                $('#optionListTable tbody tr').each(function (i, v) {
                    if ($(this).find('.NestedQuestionDiv').hasClass('NestedQuestionDiv')) {
                        $(this).find('.NestedQuestionDiv').show();
                    }
                });
                $('#NestedQuestionEditValue' + questionoptionslist[i].OptionId + '').drop("set selected", questionoptionslist[i].NestedQuestionId);
            }

            counter = questionoptionslist[i].OptionId;
        }
        var length = ($('#optionListTable tr').length) + 1;
        $('#txt_options').val('');
        $('#txt_options').attr('placeholder', 'Option ' + length);
    }

    else if (dropdownValue === "1") {
        var questionTextValue = $(el).attr('data-textBoxValue');
        $('#optionCharLimit').val(questionTextValue);
    }
    SetOptionsPosition();
    
    AdjustWidthOfTabContentOnScrollBar();
}

function SetOptionsPosition() {
    if (!codeVisibility) {
        $('#optionListTable tbody tr').each(function () {
            $(this).find('.CodeWithValue').hide();
        });
    }
    if (!nestedQuestionVisibility) {
        $('#optionListTable tbody tr').each(function () {
            $(this).find('.NestedQuestionDiv').hide();
        });
    }
    if (codeVisibility && nestedQuestionVisibility) {
        $('#optionListTable tbody tr').each(function () {
            SetNestedQuestionInNextLine($(this));
        });
    }
}

function SetOptionsWidth() {
    $('.optionText').css({ 'width': '55%' });
    $('#OptionTextDiv').css({ 'width': '58%' });
    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");
    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))  // If Internet Explorer, return version number
    {
        $('.optionText').css({ 'vertical-align': 'top' });
        $('.DeleteBtn').css({ 'vertical-align': 'top', 'padding-top':'5px' });
    }
}

function FormQuestionOptionTable(optionValue, codeVisibility, codeType, codeValue, OptionEditId, IsEdit, nestedQuestionVisibility, QId) {
    var optionTableRow = '';
    var questionOptionId = IsEdit === true ? OptionEditId : ++counter;
    optionTableRow += '<tr> <td id="' + questionOptionId + '" style="width:80%" class="optionText"><div class="mdl-textfield-questionnaire mdl-js-textfield"><div class="ui input left icon" style="width:100%">' +
        '<i class="material-icons uncheckedRadioBtn"> radio_button_unchecked</i> <input class="mdl-textfield__input body2" style="padding: 0px !important; width: auto" type="text" value="' + optionValue + '"  id="txt_Editoption" data-counter="' + questionOptionId + '"/></div></div></td>';

    optionTableRow += '<td class="DeleteBtn"><a href="#" data-keyboard="false" id="Delete" title="Delete">' +
        '<i class="material-icons">close</i></a></td>';

    if (codeVisibility) {
        if (codeValue !== undefined) {
            if ((codeValue === 0 || codeValue ==="") && codeType === "snomedCode") {
                optionTableRow += DefaultReadOrSnomedText(codeType, questionOptionId);
            }
            else {
                optionTableRow += QuestionCodeRow(codeType, codeValue, questionOptionId);
            }
        }
        else {
            optionTableRow += DefaultReadOrSnomedText(codeType, questionOptionId);
        }
    }
    if (nestedQuestionVisibility) {
        optionTableRow += NestedQuestionRow(questionOptionId, nestedQuestionList, QId);
    }
    optionTableRow += '</tr>';
    return optionTableRow;
}

function QuestionCodeRow(codeType, codeValue, questionOptionId) {
    
    var questionCodeRow = '';
    CodeValidation(codeType, codeValue);
    if (codeValid) {
        if (codeType === "readCode") {
            questionCodeRow += '<td class="CodeWithValue CodeAndLogicQuestionStyle" data-counter="' + questionOptionId + '"><div class="mdl-textfield-questionnaire mdl-js-textfield"><input class= "mdl-textfield__input body2" type = "text" id="txt_Code" value="' +
                codeValue + '" placeholder="Read code" /></div ></td>';
            Question.QuestionOptionCode = codeValue;
        }
        else {
            questionCodeRow += '<td class="CodeWithValue CodeAndLogicQuestionStyle" data-counter="' + questionOptionId + '"><div class="mdl-textfield-questionnaire mdl-js-textfield"><input class= "mdl-textfield__input body2" type = "text" id="txt_Code" value="' +
                codeValue + '" placeholder="snomed code" /></div ></td>';
            Question.QuestionSnomedOptionCode = parseInt(codeValue);
        }
    }
    return questionCodeRow;
}

function NestedQuestionRow(questionOptionId, nestedQuestionList, QId) {
    var nestedQuesTableRow = "";
    if (nestedQuestionList.length > 1) {
        nestedQuesTableRow += '<td id="' + questionOptionId + '" data-counter="' + questionOptionId + '" class="NestedQuestionDiv CodeAndLogicQuestionStyle"><div class="ui selection dropdown search" id="NestedQuestionEditValue' + questionOptionId
            + '"><input type="hidden" name="continuetonextSection" value="1"><i class="material-icons targetDemographicsDropdown" style="float: right">arrow_drop_down</i><div class="default text"></div>' +
            '<div class="menu NestedQuestionHeight"><div class="item" data-value="1">Continue to next section</div> ' +
            '<div class="item" data-value="2">Exit questionnaire</div>';
    }
    else {
        nestedQuesTableRow += '<td id="' + questionOptionId + '" data-counter="' + questionOptionId + '" class="NestedQuestionDiv CodeAndLogicQuestionStyle"><div class="ui selection dropdown search" id="NestedQuestionEditValue' + questionOptionId
            + '"><input type="hidden" name="continuetonextSection" value="1"><i class="material-icons targetDemographicsDropdown" style="float: right">arrow_drop_down</i><div class="default text"></div>' +
            '<div class="menu"><div class="item" data-value="1">Continue to next section</div> ' +
            '<div class="item" data-value="2">Exit questionnaire</div>';
    }
   
    if (nestedQuestionList.length > 0) {
        $.each(nestedQuestionList, function (i, v) {
            if (v.QuestionId != QId) {
                nestedQuesTableRow += '<div class="item" data-value="' + v.QuestionId + '">' + "Direct to " + v.QuestionText + '</div>';
            }
        });
    }
    nestedQuesTableRow += '</td>';
    return nestedQuesTableRow;

}

function DefaultReadOrSnomedText(codeType, questionOptionId) {
    if (codeType === "readCode") {
        return '<td class="CodeWithValue CodeAndLogicQuestionStyle" data-counter="' + questionOptionId + '"><div class="mdl-textfield-questionnaire mdl-js-textfield"><input class= "mdl-textfield__input body2" type="text" id="txt_Code" placeholder="Read code"/></div ></td>';
    }
    else {
        return '<td class="CodeWithValue CodeAndLogicQuestionStyle" data-counter="' + questionOptionId + '"><div class="mdl-textfield-questionnaire mdl-js-textfield"><input class= "mdl-textfield__input body2" type="text" id="txt_Code" placeholder="SNOMED code"/></div ></td>';
    }
}

function SetNestedQuestionInNextLine(thisEvent) {
    thisEvent.children().eq(1).css({ 'width': '3%' });
    thisEvent.children().eq(3).addClass("break");
    thisEvent.closest('tr').css({ 'display': 'flow-root', 'height': '80px' });
}

function ResetOptionTableStyle(thisEvent) {
    thisEvent.children().eq(1).css({ 'width': '5%' });
    thisEvent.children().eq(3).removeClass("break");
    $('#OptionTextDiv').css({ 'width': '58%' });
    thisEvent.closest('tr').css({ 'display': 'table-row', 'height': '' });
}

function AnonymousChange() {
    $('#FrequencyDiv').hide();
    $('#FrequencyDays').val(0);
    codeVisibility = false;
    $('#checkIconforTarget,#checkIconforCode').css({ 'display': 'none' });
    $('#QuestionAction').find("#MapClinicalCodes,#TargetDemo").addClass("disabled");
    $('#CodeText,#CodesDiv').css({ 'display': 'none' });
    $('#TargetDemographicsMenu,#MaptoClinicalCodesMenu').addClass('QuestionActionMenuAction');
    AdjustWidthOfTabContentOnScrollBar();
}

function AdjustWidthOfTabContentOnScrollBar() {
    hasScrollBar = $('.addEditDivStyle').get(0).scrollHeight > $('.addEditDivStyle').get(0).clientHeight;
    if (hasScrollBar) {
        $('.mdl-layout__content').css({ 'margin-left': 'calc(50% - 296px)' });
    }
    else {
        $('.mdl-layout__content').css({ 'margin-left': 'auto' });
    }
}