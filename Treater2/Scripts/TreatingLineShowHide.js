//Hides and reveals fields in Edit and Create forms depending on the Treating Line
$(function () {
    //Hides and reveals fields in Edit and Create forms depending on the Treating Line
    function settreater() {
        var t = $("#CurrentTreaterID").val();
        switch (t) {
            case "1":
                $('.ResinSolids').show();
                $('.ResinSolidsPct').hide();
                $('.Flow').show();
                $('.NetRC').hide();
                $('.ResinContentFiberesin').show();
                $('.GurleyPorosity').hide();
                $('.WetOut').hide();
                $('.Caliper').hide();
                $('.BasisWeight').hide();
                $('.TreatedPressed').show();
                $('.BarAngle').hide();
                $('.PanLevel').hide();
                $('.Paper_COA').hide();

                break;

            case "3":
                $('.ResinSolids').hide();
                $('.ResinSolidsPct').hide();
                $('.Flow').show();
                $('.NetRC').hide();
                $('.ResinContentFiberesin').show();
                $('.GurleyPorosity').hide();
                $('.WetOut').hide();
                $('.Caliper').hide();
                $('.BasisWeight').hide();
                $('.TreatedPressed').show();
                $('.BarAngle').show();
                $('.PanLevel').show();
                $('.Paper_COA').hide();

                break;

            case "4":
                $('.ResinSolids').show();
                $('.ResinSolidsPct').show();
                $('.Flow').hide();
                $('.NetRC').show();
                $('.ResinContentFiberesin').hide();
                $('.GurleyPorosity').show();
                $('.WetOut').show();
                $('.Caliper').show();
                $('.BasisWeight').show();
                $('.TreatedPressed').hide();
                $('.BarAngle').show();
                $('.PanLevel').show();

        }
    }
    $(settreater);
});
