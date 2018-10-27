module Soin

open FSharp.Data
open System.Text.RegularExpressions

//type SoinRecu(numSoin, dateSoin, montant : string) = class
type SoinRecu (numSoin, dateSoin, montant) =
    member this.NumSoin = numSoin
    member this.DateSoin = dateSoin
    member this.Montant = montant
   //let mutable NumSoin : string = numSoin
   //let mutable DateSoin : string = dateSoin
   //let mutable Montant : string = montant

let DateExpr : Regex = new Regex(@"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$")
let SoinExpr : Regex = new Regex(@"^\d{3}$") 
let MontExpr : Regex = new Regex(@"^*(\$)$") 

let ValiderSoin(numSoinRecu) : bool = 
    SoinExpr.IsMatch(numSoinRecu)

let ValiderDate(dateSoinRecu) : bool = 
    DateExpr.IsMatch(dateSoinRecu)

let ValiderMontant(montant) : bool = 
    MontExpr.IsMatch(montant)



type SoinAssure(numSoin, limite, pourcentage, limiteMensuelle) =
    member this.Soin = numSoin
    member this.Pourcentage = pourcentage
    member this.Limite = limite
    member this.LimiteMensuelle = limiteMensuelle

let SoinAssureExpr : Regex = new Regex(@"^\d{3}$") 
let PourAssurecExpr : Regex = new Regex(@"^*(\%)$")
let MontAssureExpr : Regex = new Regex(@"^*(\$)$")     

let ValiderSoinAssure(numSoin) : bool = 
    SoinAssureExpr.IsMatch(numSoin)

let ValiderLimitAssure(limite) : bool = 
    MontAssureExpr.IsMatch(limite)

let ValiderPourcentageAssure(pourcentage) : bool = 
    PourAssurecExpr.IsMatch(pourcentage) 

let ValiderLimitMensuelleAssure(limiteMensuelle) : bool = 
    MontAssureExpr.IsMatch(limiteMensuelle)
