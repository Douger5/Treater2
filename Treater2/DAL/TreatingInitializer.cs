using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Treater2.Models;

namespace Treater2.DAL
{
    public class TreatingInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TreatingContext>
    {
        protected override void Seed(TreatingContext context)
        {
            var BarConfig = new List<BarConfig>
            {
                new BarConfig{BarConfigID = "0/0"},
                new BarConfig{BarConfigID = "0/V"},
                new BarConfig{BarConfigID = "14/25"},
                new BarConfig{BarConfigID = "18/22"},
                new BarConfig{BarConfigID = "20/0"},
                new BarConfig{BarConfigID = "20/25"},
                new BarConfig{BarConfigID = "22/0"},
                new BarConfig{BarConfigID = "22/25"},
                new BarConfig{BarConfigID = "25/0"},
                new BarConfig{BarConfigID = "25/25"},
                new BarConfig{BarConfigID = "V/V"},
            };

            var TreatingSpecs = new List<TreatingSpec>
            {
                new TreatingSpec{TreatingSpecID = "C01",RCTarget=.386f,RCMax=.395f,RCMin=.375f,FlowTarget=.075f,FlowMax=.085f,FlowMin=.065f,VolTarget=.07f,VolMax=.08f,VolMin=.065f,NetRCTarget=.4f,Comment="Spec C01 Comment"},
                new TreatingSpec{TreatingSpecID = "C02",RCTarget=.36f,RCMax=.38f,RCMin=.34f,FlowTarget=.003f,FlowMax=.04f,FlowMin=.02f,VolTarget=.06f,VolMax=.065f,VolMin=.05f,NetRCTarget=.4f,Comment="Spec C02 Comment"},
                new TreatingSpec{TreatingSpecID = "C03",RCTarget=.41f,RCMax=.43f,RCMin=.39f,FlowTarget=.045f,FlowMax=.06f,FlowMin=.03f,VolTarget=.065f,VolMax=.07f,VolMin=.06f,NetRCTarget=.4f,Comment="Spec C03 Comment"}
            };
            TreatingSpecs.ForEach(ts => context.TreatingSpecs.Add(ts));
            var AshParts = new List<AshPart>
            {
                new AshPart{AshPartID="Ash047",ResinMix="M12",TreatingSpecID="C03",Size="50x50",Paper = "Kraft"},
                new AshPart{AshPartID="Ash050",ResinMix="M11",TreatingSpecID="C02",Size="40x40",Paper = "Tissue"}
            };
            AshParts.ForEach(ap => context.AshParts.Add(ap));
            var TreaterLines = new List<TreatingLine>
            {
                new TreatingLine{TreatingLineID=1, TreaterName="Melamine 1"},
                new TreatingLine{TreatingLineID=3, TreaterName="Phenolic 3"},
                new TreatingLine{TreatingLineID=4, TreaterName="Portacool"}
            };
            TreaterLines.ForEach(tl => context.TreatingLines.Add(tl));
            var TreaterTests = new List<TreaterTest>
            {
                new TreaterTest{TreatingLineID=1,TreatmentDateTime=DateTime.Parse("2002-09-01 9:00"),AshPartID="Ash050",OperatorA=541,
                    OperatorB=510,RollSkidNum="3A3",JobNum="A176513",Qty=100,UOM=UOM.ft,DryRollNum="3602C",TestNum=1,Zone1Temp=270,Zone2Temp=280,Zone3Temp=300,Speed=110,
                    PanConfig =PanConfig.W,PanLevel=10,BarConfigID = "20/25",BarAngle=95,ResinTemp=100,ResinSolids=2100,RawPaperWeight=2325,DryPaperWeight=2215,TreatedPaperWeight=3456,
                    VolatilesTreatedWeight=1537,VolatilesOvenDryWeight=1429,TreatedPressedWeight=3220, Notes="Test Notes" },
                new TreaterTest{TreatingLineID=1,TreatmentDateTime=DateTime.Parse("2017-09-10 10:00"),AshPartID="Ash050",OperatorA=541,
                    OperatorB=510,RollSkidNum="3A3",JobNum="A176513",Qty=100,UOM=UOM.ft,DryRollNum="3602C",TestNum=2,Zone1Temp=270,Zone2Temp=280,Zone3Temp=300,Speed=110,
                    PanConfig =PanConfig.W,PanLevel=10,BarConfigID = "20/25",BarAngle=95,ResinTemp=100,ResinSolids=2100,RawPaperWeight=2325,DryPaperWeight=2215,TreatedPaperWeight=3456,
                    VolatilesTreatedWeight=1537,VolatilesOvenDryWeight=1429,TreatedPressedWeight=3220, Notes="Test Notes" },
                new TreaterTest{TreatingLineID=3,TreatmentDateTime=DateTime.Parse("2017-09-10 11:00"),AshPartID="Ash047",OperatorA=541,
                    OperatorB=510,RollSkidNum="3A4",JobNum="A176514",Qty=100,UOM=UOM.ft,DryRollNum="3602C",TestNum=1,Zone1Temp=270,Zone2Temp=280,Zone3Temp=300,Speed=110,
                    PanConfig =PanConfig.W,PanLevel=10,BarConfigID = "20/25",BarAngle=95,ResinTemp=100,ResinSolids=2100,RawPaperWeight=2325,DryPaperWeight=2215,TreatedPaperWeight=3456,
                    VolatilesTreatedWeight=1537,VolatilesOvenDryWeight=1429,TreatedPressedWeight=3220, Notes="Test Notes" },
                new TreaterTest{TreatingLineID=3,TreatmentDateTime=DateTime.Parse("2017-09-10 12:00"),AshPartID="Ash050",OperatorA=541,
                    OperatorB=510,RollSkidNum="3A5",JobNum="A176515",Qty=100,UOM=UOM.ft,DryRollNum="3602C",TestNum=1,Zone1Temp=270,Zone2Temp=280,Zone3Temp=300,Speed=110,
                    PanConfig =PanConfig.W,PanLevel=10,BarConfigID = "20/25",BarAngle=95,ResinTemp=100,ResinSolids=2100,RawPaperWeight=2325,DryPaperWeight=2215,TreatedPaperWeight=3456,
                    VolatilesTreatedWeight=1537,VolatilesOvenDryWeight=1429,TreatedPressedWeight=3220, Notes="Test Notes" }
            };
            TreaterTests.ForEach(tt => context.TreaterTests.Add(tt));

            var Items = new List<Item>
            {
                new Item{ItemID="710913",Description="Release, LC-19 Coated Caul 50.25x102"},
                new Item{ItemID="710916",Description="Release,LC-3 50x99"},
                new Item{ItemID="710925",Description="Release,Phenolic Peterson 50x102"}
            };
            Items.ForEach(it => context.Items.Add(it));

            var ReleaseSheets = new List<ReleaseSheet>
            {
                new ReleaseSheet{ItemID="710913",ReleaseDateTime=DateTime.Parse("2017-09-10 09:00"),Job = "A176631",Board="A", Width=50,Length=102,Roll="221A",Lbs=156,Sheets=300,Notes="Hello Release",Rejected=5},
                new ReleaseSheet{ItemID="710913",ReleaseDateTime=DateTime.Parse("2017-09-10 10:00"),Job = "A176631",Board="B", Width=50,Length=102,Roll="221A",Lbs=156,Sheets=200,Notes="Hello Release 2",Rejected=2},
                new ReleaseSheet{ItemID="710925",ReleaseDateTime=DateTime.Parse("2017-09-10 11:00"),Job = "A176631",Board="A", Width=50,Length=102,Roll="225",Lbs=156,Sheets=300,Notes="Hello Release 3",Rejected=0}
            };
            ReleaseSheets.ForEach(r => context.ReleaseSheets.Add(r));


            context.SaveChanges();
        }
    }
}