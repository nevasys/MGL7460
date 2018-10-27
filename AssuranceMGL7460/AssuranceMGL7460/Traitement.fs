module Traitement

open System.IO
open FSharp.Data
open Soin
open Police


type PoliceSoinsAssures = JsonProvider<"./ressources/polices.json">
type ReclamationSoinsAssures = JsonProvider<"./ressources/input1.json">


let leTraitement () =

    let itemsPolice = PoliceSoinsAssures.Parse(File.ReadAllText("./ressources/polices.json"))
    let itemsReclamation = ReclamationSoinsAssures.Parse(File.ReadAllText("./ressources/input1.json"))

    let mutable lesDeSoinAssure : SoinAssure list = []
    let mutable leseDeSoinRecu : SoinRecu list = []

    let PoliceContratRecu = new Police(itemsReclamation.Dossier, itemsReclamation.Mois)

    for demandeReclamation in itemsReclamation.Reclamations do
        leseDeSoinRecu <- new SoinRecu(demandeReclamation.Soin, demandeReclamation.Date, demandeReclamation.Montant) :: leseDeSoinRecu

    let listeDesSoinsRecu  = leseDeSoinRecu |> List.toArray


    for contract in itemsPolice.Police do
        if(contract.Contrat = PoliceContratRecu.Dossier.ToString().[0].ToString()) then
            for care in contract.Soins do
                lesDeSoinAssure <- new SoinAssure(care.Soin, care.Pourcentage, care.Limite, care.LimiteMensuelle) :: lesDeSoinAssure

    let listeDesSoinsAssurer  = lesDeSoinAssure |> List.toArray


    //*****Validation*****
    let contratValide = 
        if not (dossierValide(PoliceContratRecu.Dossier.ToString())) then printfn "Dossier non valide"

    let soinRecuValide = 
        for y in listeDesSoinsRecu do
            if not (ValiderSoinRecu(y.NumSoin.ToString())) then printfn "Soin %A est non valide" y.NumSoin
            if not (ValiderDateRecu(y.DateSoin.ToString())) then printfn "Date %A du soin %A est non valide" y.DateSoin y.NumSoin
            if not (ValiderMoisReclamation(PoliceContratRecu.Mois, y.DateSoin)) then printfn "Reclamation d'un autre mois pour le soin : %A" y.NumSoin
            if not (ValiderMontantRecu(y.Montant.ToString())) then printfn "Montant du soin %A est non valide" y.NumSoin

    //*****Taitement des données*****
    let LettreTypeDeContratTemp = PoliceContratRecu.Dossier.ToString()
    let LettreTypeDeContrat = LettreTypeDeContratTemp.[0]





    printfn "Liste des soins reclamé pour le contrat : %A" PoliceContratRecu.Dossier
    //for y in listeDesSoinsRecu do
        //printfn "Soin reclamé: %A" y.NumSoin
        //printfn "Montant reclamé: %A" y.Montant

    printfn "Liste des soins assuré pour le contrat : %c"  LettreTypeDeContrat
    //for x in listeDesSoinsAssurer do
        //printfn "Soin assuré : %A" x.Soin
        //printfn "Pourcentage assuré : %A" x.Pourcentage
        //printfn "Limite assuré : %A" x.Limite
        //printfn "LimiteMensuelle assuré : %A" x.LimiteMensuelle

   

