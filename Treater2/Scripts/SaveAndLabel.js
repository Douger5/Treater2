$(function () {
    $(openlabel);
    function openlabel() {
        if (sheetid !== "0" && sheetid !== "") {
            window.open(url);
            sheetid = 0;
        }
    }
});
            
