module Soin

open FSharp.Data
open System.Text.RegularExpressions

type SoinRecu(numSoin, dateSoin, montant : string) = class
   let mutable NumSoin : string = numSoin
   let mutable DateSoin : string = dateSoin
   let mutable Montant : string = montant

   let DateExpr : Regex = new Regex(@"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$")
   let SoinExpr : Regex = new Regex(@"^\d{3}$") 
   let MontExpr : Regex = new Regex(@"^*(\$)$") 

   member this.ValiderSoin() : bool = 
     SoinExpr.IsMatch(NumSoin)

   member this.ValiderDate() : bool = 
     DateExpr.IsMatch(DateSoin)

   member this.ValiderMontant() : bool = 
     MontExpr.IsMatch(Montant)
end


type SoinAssure(numSoin, limit, pourcentage, limitMansuel) = class
   let NumSoin : string = numSoin
   let Limit : string = limit
   let Pourcentage : string = pourcentage
   let LimitMansuel : string = limitMansuel


   let SoinExpr : Regex = new Regex(@"^\d{3}$") 
   let PourcExpr : Regex = new Regex(@"^*(\%)$")
   let MontExpr : Regex = new Regex(@"^*(\$)$")     

   member this.ValiderSoin() : bool = 
     SoinExpr.IsMatch(NumSoin)

   member this.ValiderLimit() : bool = 
     MontExpr.IsMatch(Limit)

   member this.ValiderPourcentage() : bool = 
     PourcExpr.IsMatch(Pourcentage) 

   member this.ValiderLimitMansuel() : bool = 
     MontExpr.IsMatch(LimitMansuel)
end