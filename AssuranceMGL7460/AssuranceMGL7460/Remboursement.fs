module Remboursement

type Remboursement (numSoin, dateSoin, montant : decimal) =
    member this.NumSoin = numSoin
    member this.DateSoin = dateSoin
    member this.Montant = montant