module SoinsAssurer

type SoinsAssurer (soin, pourcentage, limite, limiteMensuelle) =
    member this.Soin = soin
    member this.Pourcentage = pourcentage
    member this.Limite = limite
    member this.LimiteMensuelle = limiteMensuelle