module Traitement

open System.IO
open FSharp.Data
open Soin
open SoinsAssurer
open SoinsReclamer
open Police


type PoliceSoinsAssures = JsonProvider<"./ressources/polices.json">
type ReclamationSoinsAssures = JsonProvider<"./ressources/input1.json">

//let value = JsonValue.Load("./ressources/polices.json")

let leTraitement () =

    let itemsPolice = PoliceSoinsAssures.Parse(File.ReadAllText("./ressources/polices.json"))
    let itemsReclamation = ReclamationSoinsAssures.Parse(File.ReadAllText("./ressources/input1.json"))

    let mutable listeDeSoinAssure : SoinsAssurer list = []
    let mutable listeDeSoinRecu : SoinRecu list = []

    let PoliceContratRecu = new Police(itemsReclamation.Dossier, itemsReclamation.Mois)

    for demandeReclamation in itemsReclamation.Reclamations do
        listeDeSoinRecu <- new SoinRecu(demandeReclamation.Soin.ToString(), demandeReclamation.Date.ToString(), demandeReclamation.Montant.ToString()) :: listeDeSoinRecu
                //printfn "Soins : %A" care.Soin
                //printfn "Pourcentage : %f" care.Pourcentage

    let lesSoinsRecu  = listeDeSoinRecu |> List.toArray


    for contract in itemsPolice.Police do
        if(contract.Contrat = PoliceContratRecu.Dossier.ToString().[0].ToString()) then
            for care in contract.Soins do
                listeDeSoinAssure <- new SoinsAssurer(care.Soin, care.Pourcentage, care.Limite, care.LimiteMensuelle) :: listeDeSoinAssure
                //printfn "Soins : %A" care.Soin
                //printfn "Pourcentage : %f" care.Pourcentage

    let lesSoinsAssurer  = listeDeSoinAssure |> List.toArray

    let temp = PoliceContratRecu.Dossier.ToString()
    let policeContrat = temp.[0]

    printfn "Liste des soins reclamé pour le contrat : %A" PoliceContratRecu.Dossier
    for y in lesSoinsRecu do
        printfn "Soin reclamé: %A" y.NumSoin
        printfn "Montant reclamé: %A" y.Montant

    printfn "Liste des soins assuré pour le contrat : %c"  policeContrat
    for x in lesSoinsAssurer do
        printfn "Soin assuré : %A" x.Soin
        printfn "Pourcentage assuré : %A" x.Pourcentage
        printfn "Limite assuré : %A" x.Limite
        printfn "LimiteMensuelle assuré : %A" x.LimiteMensuelle

   

