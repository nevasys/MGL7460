module Police

type Police (dossier, mois) =
    member this.Dossier = dossier
    member this.Mois = mois
    //member this.Contrat = dossier[0].ToString()
    