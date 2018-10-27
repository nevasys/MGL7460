module Traitement

open System.IO
open FSharp.Data
open SoinsAssurer
open SoinsReclamer
open Police


type PoliceSoinsAssures = JsonProvider<"./ressources/polices.json">
type ReclamationSoinsAssures = JsonProvider<"./ressources/input1.json">

//let value = JsonValue.Load("./ressources/polices.json")

let leTraitement () =

    let itemsPolice = PoliceSoinsAssures.Parse(File.ReadAllText("./ressources/polices.json"))
    let itemsReclamation = ReclamationSoinsAssures.Parse(File.ReadAllText("./ressources/input1.json"))

    let mutable listOfCare : SoinsAssurer list = []
    let mutable listOfTreatment : SoinsReclamer list = []

    let PoliceContratReclamer = new Police(itemsReclamation.Dossier, itemsReclamation.Mois)

    for demandeReclamation in itemsReclamation.Reclamations do
        listOfTreatment <- new SoinsReclamer(demandeReclamation.Soin, demandeReclamation.Date, demandeReclamation.Montant) :: listOfTreatment
                //printfn "Soins : %A" care.Soin
                //printfn "Pourcentage : %f" care.Pourcentage

    let lesSoinsReclamer  = listOfTreatment |> List.toArray


    for contract in itemsPolice.Police do
        if(contract.Contrat = PoliceContratReclamer.Dossier.ToString().[0].ToString()) then
            for care in contract.Soins do
                listOfCare <- new SoinsAssurer(care.Soin, care.Pourcentage, care.Limite, care.LimiteMensuelle) :: listOfCare
                //printfn "Soins : %A" care.Soin
                //printfn "Pourcentage : %f" care.Pourcentage

    let lesSoinsAssurer  = listOfCare |> List.toArray

    let temp = PoliceContratReclamer.Dossier.ToString()
    let policeContrat = temp.[0]

    printfn "Liste des soins reclamé pour le contrat : %A" PoliceContratReclamer.Dossier
    for y in lesSoinsReclamer do
        printfn "Soin reclamé: %A" y.Soin
        printfn "Montant reclamé: %A" y.Montant

    printfn "Liste des soins assuré pour le contrat : %c"  policeContrat
    for x in lesSoinsAssurer do
        printfn "Soin assuré : %A" x.Soin
        printfn "Pourcentage assuré : %A" x.Pourcentage
        printfn "Limite assuré : %A" x.Limite
        printfn "LimiteMensuelle assuré : %A" x.LimiteMensuelle

   

