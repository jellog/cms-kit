var jellog = jellog || {};
$(function () {
    jellog.modals.newPollButton = function () {
        var initModal = function () {
            var l = jellog.localization.getResource("CmsKit");
            var pollIndex = $('#OptionStartIndex').val();

            var entityMap = {
                '&': '&amp;',
                '<': '&lt;',
                '>': '&gt;',
                '"': '&quot;',
                "'": '&#39;',
                '/': '&#x2F;',
                '`': '&#x60;',
                '=': '&#x3D;'
            };

            function escapeHtml(string) {
                return String(string).replace(/[&<>"'`=\/]/g, function (s) {
                    return entityMap[s];
                });
            }

            var getOptionTableRow = function (text) {
                var escapedHtml = escapeHtml(text);
                return "<tr>\r\n<td>\r\n" + escapedHtml + "\r\n </td>" +
                    "<td class=\"text-end\">" +
                    "                                       <button type=\"button\" class=\"btn btn-outline-dark createUpOptionButton\"><i class=\"fa fa-arrow-up\"></i></button>" +
                    "                                       <button type=\"button\" class=\"btn btn-outline-dark createDownOptionButton\"><i class=\"fa fa-arrow-down\"></i></button>" +
                    "                                       <button type=\"button\" class=\"btn btn-outline-danger createDeleteOptionButton\"><i class=\"fa fa-trash\"></i></button>" +
                    "</td > " +
                    "                                        <td hidden >" +
                    "                                       <input type=\"text\" name=\"ViewModel.PollOptions[" + pollIndex + "].Text\" value=\"" + text + "\"/>" +
                    "                                       <input type=\"text\" name=\"ViewModel.PollOptions[" + pollIndex + "].Order\" value=\"" + (Number(pollIndex) + 1) + "\"id=\"" + text + "\">" +
                    "</td></tr>";
            }

            var addTextToTableRow = function () {
                var optionText = $("#NewOption").val();

                if (!optionText) {
                    return;
                }

                $("#NewOption").val("");

                var html = getOptionTableRow(optionText);

                $("#CreateOptionTableBodyId").append(html);

                pollIndex++;
                $("#CreateOptionTableId").show();
            }

            $("#CreateAddNewOptionButton").on('click', function () {
                addTextToTableRow();
            });

            function isDoubleClicked(element) {
                if (element.data("isclicked")) return true;

                element.data("isclicked", true);
                setTimeout(function () {
                    element.removeData("isclicked");
                }, 500);
            }

            $("#NewOption").keypress(function (event) {
                let keyCode = (event.keyCode ? event.keyCode : event.which);
                if (keyCode == "13") {
                    addTextToTableRow();
                    return false;
                }
            })

            $(document).on('click', '.createDeleteOptionButton', function () {
                var tag = $(this).parent().parent();

                var inputs = tag.find("input");
                $(inputs).each(function (i) {
                    $(this).val("");
                });

                tag.hide();
            });

            $(document).on('click', '.createUpOptionButton', function () {

                if (!$(this).closest("tr").is(":first-child")) {

                    if (isDoubleClicked($(this))) return;

                    var row = $(this).parents("tr:first");
                    var $thirdHiddenInput2 = row.find('td').eq(2);
                    var hiddenInput = $thirdHiddenInput2.find('input')[1];

                    var name = "input[id='" + hiddenInput.id + "']";
                    $(name).attr('value', --hiddenInput.value);

                    var previousRow = row.prev();
                    var $previousHiddenInput = previousRow.find('td').eq(2);
                    var previousHiddenInput = $previousHiddenInput.find('input')[1];

                    var previosuName = "input[id='" + previousHiddenInput.id + "']";
                    $(previosuName).attr('value', ++previousHiddenInput.value);
                    row.insertBefore(row.prev());
                }

            });

            $(document).on('click', '.createDownOptionButton', function () {


                if (!$(this).closest("tr").is(":last-child")) {

                    if (isDoubleClicked($(this))) return;

                    var row = $(this).parents("tr:first");
                    var $thirdHiddenInput2 = row.find('td').eq(2);
                    var hiddenInput = $thirdHiddenInput2.find('input')[1];

                    var name = "input[id='" + hiddenInput.id + "']";
                    $(name).attr('value', ++hiddenInput.value);

                    var nextRow = row.next();
                    var $nextHiddenInput = nextRow.find('td').eq(2);
                    var nextHiddenInput = $nextHiddenInput.find('input')[1];

                    var previosuName = "input[id='" + nextHiddenInput.id + "']";
                    $(previosuName).attr('value', --nextHiddenInput.value);

                    row.insertAfter(row.next());
                }
            });
        };

        return {
            initModal: initModal
        };
    };
});