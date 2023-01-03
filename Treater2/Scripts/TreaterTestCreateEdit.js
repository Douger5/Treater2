$(function () {
    function toPercent(i) {
        return (100 * i).toFixed(1) + " %"
    }
    $(updateashpart);
    $(alertonly);
    $("#AshPartID").change(updateashpart);
    $("#RawPaperWeight, #DryPaperWeight, #VolatilesTreatedWeight, #VolatilesOvenDryWeight, #TreatedPaperWeight, #TreatedPressedWeight, #ResinSolids").change(recalc);
    $("#AshPartID, #GurleyPorosity, #WetOut, #Caliper").change(alertonly);
    $(selectPan);
    $(removeZeros);

    function removeZeros() {
        if ($("#VolatilesTreatedWeight").val() == "0") {
            $("#VolatilesTreatedWeight").val("");
        }
        if ($("#TreatedPaperWeight").val() == "0") {
            $("#TreatedPaperWeight").val("");
        }
        if ($("#TreatedPressedWeight").val() == "0") {
            $("#TreatedPressedWeight").val("");
        }
        if ($("#VolatilesOvenDryWeight").val() == "0") {
            $("#VolatilesOvenDryWeight").val("");
        }
        if ($("#RawPaperWeight").val() == "0") {
            $("#RawPaperWeight").val("");
        }
        if ($("#DryPaperWeight").val() == "0") {
            $("#DryPaperWeight").val("");
        }
    }

    function selectPan() {
        var pan = $("#panConfigVal").val();
        if (pan) {
            if (pan !== "-1") {
                $("#PanConfig").val(pan);
            }
        }
    }

    function CalcMC(rpw, dpw) {
        var MC = 0;
        if (rpw !== 0) {
            MC = Math.round((rpw - dpw)*1000 / rpw)/1000;
            //MC = (rpw - dpw) / rpw;
        }
        return MC
    }

    function recalc() {

        //Set RawPaperMC
        var t = CalcMC($("#RawPaperWeight").val(), $("#DryPaperWeight").val());
        $("#RawPaperMC").val(toPercent(t));

        //Set Vol and alert red if Vol not in spec
        t = CalcMC($("#VolatilesTreatedWeight").val(), $("#VolatilesOvenDryWeight").val());
        var bmax = parseFloat($("#AshPart_TreatingSpec_VolMax").val()) / 100.0;
        var bmin = parseFloat($("#AshPart_TreatingSpec_VolMin").val()) / 100.0;
        if (t > bmax || t < bmin) {
            $("#Vol").css('color', 'red');
        } else {
            $("#Vol").css('color', 'black');
        }
        $("#Vol").val(toPercent(t));

        //Set ResinContentFiberesin and alert red if not in spec
        var tl = $("#CurrentTreaterID").val();
        switch (tl) {
            case "1":
                t = CalcMC(($("#VolatilesTreatedWeight").val() * 282 / 125), $("#DryPaperWeight").val());
                break;
            case "4":
                t = CalcMC(($("#TreatedPaperWeight").val() * 9 / 4), $("#DryPaperWeight").val());
                break;
            default:
                t = CalcMC($("#TreatedPaperWeight").val(), $("#DryPaperWeight").val());
        }
        bmax = parseFloat($("#AshPart_TreatingSpec_RCMax").val()) / 100.0;
        bmin = parseFloat($("#AshPart_TreatingSpec_RCMin").val()) / 100.0;
        if (t > bmax || t < bmin) {
            $("#ResinContentFiberesin").css('color', 'red');
        } else {
            $("#ResinContentFiberesin").css('color', 'black');
        }
        $("#ResinContentFiberesin").val(toPercent(t));

        //Set NetRC and alert red if not in spec
        t = (parseFloat($("#ResinContentFiberesin").val()) - parseFloat($("#Vol").val())) / 100.0;
        bmax = parseFloat($("#AshPart_TreatingSpec_NetRCMax").val()) / 100.0;
        bmin = parseFloat($("#AshPart_TreatingSpec_NetRCMin").val()) / 100.0;
        if (t > bmax || t < bmin) {
            $("#NetRC").css('color', 'red');
        } else {
            $("#NetRC").css('color', 'black');
        }
        $("#NetRC").val(toPercent(t));

        //Set Flow and alert red if not in spec
        t = CalcMC($("#TreatedPaperWeight").val(), $("#TreatedPressedWeight").val());
        bmax = parseFloat($("#AshPart_TreatingSpec_FlowMax").val()) / 100.0;
        bmin = parseFloat($("#AshPart_TreatingSpec_FlowMin").val()) / 100.0;
        if (t > bmax || t < bmin) {
            $("#Flow").css('color', 'red');
        } else {
            $("#Flow").css('color', 'black');
        }
        $("#Flow").val(toPercent(t));

        //Calc & Set ResinSolidsPct
        if ($("#ResinSolids").val() !== 0) {
            t = (parseFloat($("#ResinSolids").val()) - 1.3251) / 0.34;
            $("#ResinSolidsPct").val(toPercent(t));
        }

        //Calc & Set Basis Weight
        t = 0.06614 * ($("#TreatedPaperWeight").val() * 9 / 4);
        bmax = parseFloat($("#AshPart_TreatingSpec_BasisWeightMax").val());
        bmin = parseFloat($("#AshPart_TreatingSpec_BasisWeightMin").val());
        if (t > bmax || t < bmin) {
            $("#BasisWeight").css('color', 'red');
        } else {
            $("#BasisWeight").css('color', 'black');
        }
        $("#BasisWeight").val(t.toFixed(1));
    };
    function alertonly() {
        //Alert Gurley
        t = parseFloat($("#GurleyPorosity").val());
        var bmax = parseFloat($("#AshPart_TreatingSpec_GurleyPorosityMax").val());
        var bmin = parseFloat($("#AshPart_TreatingSpec_GurleyPorosityMin").val());
        if (t > bmax || t < bmin) {
            $("#GurleyPorosity").css('color', 'red');
        } else {
            $("#GurleyPorosity").css('color', 'black');
        }

        //Alert WetOut
        t = parseFloat($("#WetOut").val());
        bmax = parseFloat($("#AshPart_TreatingSpec_WetOutMax").val());
        bmin = parseFloat($("#AshPart_TreatingSpec_WetOutMin").val());
        if (t > bmax || t < bmin) {
            $("#WetOut").css('color', 'red');
        } else {
            $("#WetOut").css('color', 'black');
        }

        //Alert Caliper
        t = parseFloat($("#Caliper").val());
        bmax = parseFloat($("#AshPart_TreatingSpec_CaliperMax").val());
        bmin = parseFloat($("#AshPart_TreatingSpec_CaliperMin").val());
        if (t > bmax || t < bmin) {
            $("#Caliper").css('color', 'red');
        } else {
            $("#Caliper").css('color', 'black');
        }
    };

    //Updates the recipees and specs when a new AshPart is selected
    function updateashpart () {
        var t = $("#AshPartID").val();
        if (t !== "") {
            var url = $("#loader").data('request-url') + '?id=' + t;
            $.post(url, function (res) {
                if (res.Success === "true") {

                    //enable the text boxes and set the value

                    $("#AshPart_TreatingSpecID").prop('disabled', true).val(res.Data.AshPart.TreatingSpecID);
                    $("#AshPart_ResinMix").prop('disabled', true).val(res.Data.AshPart.ResinMix);
                    $("#AshPart_Paper").prop('disabled', true).val(res.Data.AshPart.Paper);
                    $("#AshPart_TreatingSpec_RCMin").prop('disabled', true).val(toPercent(res.Data.AshPart.TreatingSpec.RCMin));
                    $("#AshPart_TreatingSpec_RCMax").prop('disabled', true).val(toPercent(res.Data.AshPart.TreatingSpec.RCMax));
                    $("#AshPart_TreatingSpec_RCTarget").prop('disabled', true).val(toPercent(res.Data.AshPart.TreatingSpec.RCTarget));
                    $("#AshPart_TreatingSpec_VolMin").prop('disabled', true).val(toPercent(res.Data.AshPart.TreatingSpec.VolMin));
                    $("#AshPart_TreatingSpec_VolMax").prop('disabled', true).val(toPercent(res.Data.AshPart.TreatingSpec.VolMax));
                    $("#AshPart_TreatingSpec_VolTarget").prop('disabled', true).val(toPercent(res.Data.AshPart.TreatingSpec.VolTarget));
                    $("#AshPart_TreatingSpec_FlowMin").prop('disabled', true).val(toPercent(res.Data.AshPart.TreatingSpec.FlowMin));
                    $("#AshPart_TreatingSpec_FlowMax").prop('disabled', true).val(toPercent(res.Data.AshPart.TreatingSpec.FlowMax));
                    $("#AshPart_TreatingSpec_FlowTarget").prop('disabled', true).val(toPercent(res.Data.AshPart.TreatingSpec.FlowTarget));
                    $("#AshPart_TreatingSpec_NetRCMin").prop('disabled', true).val(toPercent(res.Data.AshPart.TreatingSpec.NetRCMin));
                    $("#AshPart_TreatingSpec_NetRCMax").prop('disabled', true).val(toPercent(res.Data.AshPart.TreatingSpec.NetRCMax));
                    $("#AshPart_TreatingSpec_NetRCTarget").prop('disabled', true).val(toPercent(res.Data.AshPart.TreatingSpec.NetRCTarget));
                    $("#AshPart_TreatingSpec_GurleyPorosityMin").prop('disabled', true).val(res.Data.AshPart.TreatingSpec.GurleyPorosityMin.toFixed(1));
                    $("#AshPart_TreatingSpec_GurleyPorosityMax").prop('disabled', true).val(res.Data.AshPart.TreatingSpec.GurleyPorosityMax.toFixed(1));
                    $("#AshPart_TreatingSpec_GurleyPorosityTarget").prop('disabled', true).val(res.Data.AshPart.TreatingSpec.GurleyPorosityTarget.toFixed(1));
                    $("#AshPart_TreatingSpec_WetOutMin").prop('disabled', true).val(res.Data.AshPart.TreatingSpec.WetOutMin);
                    $("#AshPart_TreatingSpec_WetOutMax").prop('disabled', true).val(res.Data.AshPart.TreatingSpec.WetOutMax);
                    $("#AshPart_TreatingSpec_WetOutTarget").prop('disabled', true).val(res.Data.AshPart.TreatingSpec.WetOutTarget);
                    $("#AshPart_TreatingSpec_CaliperMin").prop('disabled', true).val(res.Data.AshPart.TreatingSpec.CaliperMin);
                    $("#AshPart_TreatingSpec_CaliperMax").prop('disabled', true).val(res.Data.AshPart.TreatingSpec.CaliperMax);
                    $("#AshPart_TreatingSpec_CaliperTarget").prop('disabled', true).val(res.Data.AshPart.TreatingSpec.CaliperTarget);
                    $("#AshPart_TreatingSpec_BasisWeightMin").prop('disabled', true).val(res.Data.AshPart.TreatingSpec.BasisWeightMin);
                    $("#AshPart_TreatingSpec_BasisWeightMax").prop('disabled', true).val(res.Data.AshPart.TreatingSpec.BasisWeightMax);
                    $("#AshPart_TreatingSpec_BasisWeightTarget").prop('disabled', true).val(res.Data.AshPart.TreatingSpec.BasisWeightTarget);
                    $("#AshPart_Size").prop('disabled', true).val(res.Data.AshPart.Size);
                    $(recalc);
                } else {
                    alert("Error getting data!");
                }
            });
        } else {
            //Let's clear the values and disable :)
            $("#AshPart_TreatingSpecID").val('');
            $("#AshPart_ResinMix").val('');
            $("#AshPart_Paper").val('');
            $("#AshPart_TreatingSpec_RCMin").val(0);
            $("#AshPart_TreatingSpec_RCMax").val(0);
            $("#AshPart_TreatingSpec_VolMin").val(0);
            $("#AshPart_TreatingSpec_VolMax").val(0);
            $("#AshPart_TreatingSpec_FlowMin").val(0);
            $("#AshPart_TreatingSpec_FlowMax").val(0);
            $("#AshPart_Size").val(0);
        }

    };
});