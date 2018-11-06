module Police
open System.Text.RegularExpressions
open System

type Police (dossier, mois : DateTime) =
    member this.Dossier = dossier
    member this.Mois = mois
    
let validerDossierRegex = new Regex(@"^[ABCDE]\d{6}$")
let validerMoisReclamationRegex = new Regex(@"\d{4}-(0[1-9]|1[0-2])")

let dossierValide(dossier) : bool = 
    validerDossierRegex.IsMatch(dossier)

let ValiderMoisReclamation(dateDossier : DateTime, datesSoin : DateTime) : bool = 
    dateDossier.Month <> datesSoin.Month