module JSONHash

open System.IO
open FSharp.Data
open System.Text.RegularExpressions
open Traitement

type fichierJSON (fichierInput, fichierOutput) =
    member this.fichierInput = fichierInput
    member this.fichierOutput = fichierOutput

    //let fichierJSON = "./ressources/input1.json";

type Reclamation = JsonProvider<"./ressources/input1.json">

let lireFichierReclamation(fichierInput) =

    //let fichierJson = "./ressources/" + fichierInput

    let items = Reclamation.Parse(File.ReadAllText("./ressources/input1.json"))

    //let validerDossierRegex = new Regex(@"^[ABCDE]\d{6}$")
    //let validerMoisReclamationRegex = new Regex(@"\d{4}-(0[1-9]|1[0-2])")
    //let validerMoisSoinRegex = new Regex(@"^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])$")
    //let validerMontantRegex = new Regex(@"^\d+[.,]?\d{2}\$$")
     
 
    //let dossierValide = validerDossierRegex.IsMatch(items.Dossier)
    //let moisValide = validerMoisReclamationRegex.IsMatch(items.Mois.ToString())

    //let listerLesReclamations = 
        //for reclamation in items.Reclamations do
            //let lesReclamations = new Reclamation(items.Dossier reclamation.Date, reclamation.Soin)
    //lireFichierPolice(fichierInput)

    leTraitement()

    //let reclamationMoisValide =
        //for reclamation in items.Reclamations do
            //if(reclamation.Date.Month <> items.Mois.Month) then printfn "Reclamation d'un autre mois pour le soin : %i" reclamation.Soin

    //************Validation du montant à revoir  **********************
    //reclamation.Montant.ToString() --> est non fonctionnel
    //let MontantNonValide =
        //for reclamation in items.Reclamations do
            //if(validerMontantRegex.IsMatch(reclamation.Montant.ToString())) then printfn "Le montant du soin %i n'est pas valide" reclamation.Soin

    //let soinMoisValide =
        //for reclamation in items.Reclamations do
            //if(validerMoisSoinRegex.IsMatch(reclamation.Date.ToString())) then 
                //printfn "Le mois du soin %i n'est pas valide" reclamation.Soin
                //printfn "MOIS = %A" reclamation.Date


    //printfn "Validité du dossier : %A" dossierValide
    //printfn "Validité du mois : %A" moisValide



