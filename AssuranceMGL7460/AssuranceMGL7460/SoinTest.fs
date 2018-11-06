module SoinTest

open Soin
open Xunit

[<Fact>]
let Test_ValiderSoinRecu_doit_etre_vrai() =
    Assert.True(ValiderSoinRecu("100"))