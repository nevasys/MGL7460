module Traitement

open System.IO
open FSharp.Data
open Soin
open Police
open Remboursement
open System
open N

type PoliceSoinsAssures = JsonProvider<"./ressources/polices.json">
type ReclamationSoinsAssures = JsonProvider<"./ressources/input1.json">



let leTraitement () =

    let itemsPolice = PoliceSoinsAssures.Parse(File.ReadAllText("./ressources/polices.json"))
    let itemsReclamation = ReclamationSoinsAssures.Parse(File.ReadAllText("./ressources/input1.json"))

    let mutable lesSoinAssure : SoinAssure list = []
    let mutable lesSoinRecu : SoinRecu list = []
    let mutable lesSoinRembourse : Remboursement list = []

    let PoliceContratRecu = new Police(itemsReclamation.Dossier, itemsReclamation.Mois)

    for demandeReclamation in itemsReclamation.Reclamations do
        lesSoinRecu <- new SoinRecu(demandeReclamation.Soin, demandeReclamation.Date, demandeReclamation.Montant) :: lesSoinRecu

    let listeDesSoinsRecu  = lesSoinRecu |> List.toArray


    for contract in itemsPolice.Police do
        if(contract.Contrat = PoliceContratRecu.Dossier.ToString().[0].ToString()) then
            for care in contract.Soins do
                lesSoinAssure <- new SoinAssure(care.Soin, care.Pourcentage, care.Limite, care.LimiteMensuelle) :: lesSoinAssure

    let listeDesSoinsAssurer  = lesSoinAssure |> List.toArray


    //*****Validation*****
    let contratValide = 
        if not (dossierValide(PoliceContratRecu.Dossier.ToString())) then printfn "Dossier non valide"

    let soinRecuValide = 
        for y in listeDesSoinsRecu do
            if not (ValiderSoinRecu(y.NumSoin.ToString())) then printfn "Soin %A est non valide" y.NumSoin
            if not (ValiderDateRecu(y.DateSoin.ToString())) then printfn "Date %A du soin %A est non valide" y.DateSoin y.NumSoin
            if (ValiderMoisReclamation(PoliceContratRecu.Mois, y.DateSoin)) then printfn "Reclamation d'un autre mois pour le soin : %A" y.NumSoin
            //if not (ValiderMontantRecu(y.Montant.ToString())) then printfn "Montant '%A' du soin %A est non valide" y.Montant y.NumSoin

    //*****Taitement des données*****
    let calculerRemboursement = 
        for itemsAssure in listeDesSoinsAssurer do
            for itemsRecu in listeDesSoinsRecu do
                if(itemsAssure.Soin = itemsRecu.NumSoin) then 
                    let montantRemboursement : decimal = Decimal.Parse(itemsRecu.Montant.ToString()) * itemsAssure.Pourcentage
                    
                    if((itemsAssure.HasLimiteMensuelle=true) And montantRemboursement > Decimal.Parse(itemsAssure.LimiteMensuelle.ToString())) then
                        let montantRemboursement = Decimal.Parse(itemsAssure.LimiteMensuelle.ToString())
                          

                    if(montantRemboursement < Decimal.Parse(itemsAssure.Limite.ToString()) && itemsAssure.Limite <> 0) then 
                        lesSoinRembourse <- new Remboursement(itemsRecu.NumSoin, itemsRecu.DateSoin, montantRemboursement) :: lesSoinRembourse
                        let itemsAssure.LimiteMensuelle =  itemsAssure.LimiteMensuelle - montantRemboursement
                        itemsAssure.LimiteMensuelle
                    else
                        lesSoinRembourse <- new Remboursement(itemsRecu.NumSoin, itemsRecu.DateSoin, Decimal.Parse(itemsAssure.Limite.ToString())) :: lesSoinRembourse
                        let itemsAssure.LimiteMensuelle =  itemsAssure.LimiteMensuelle - Decimal.Parse(itemsAssure.Limite.ToString())
                        itemsAssure.LimiteMensuelle


    let listeDesRemboursement  = lesSoinRembourse |> List.toArray

    let mutable total:decimal = 0.0M
    let calculMontantTotal =
        for remboursements in listeDesRemboursement do
            total <- remboursements.Montant + total


    //****Affichage à la console****
    let LettreTypeDeContratTemp = PoliceContratRecu.Dossier.ToString()
    let LettreTypeDeContrat = LettreTypeDeContratTemp.[0]

    printfn "Dossier : %A" PoliceContratRecu.Dossier

    printfn "Mois : %A"  PoliceContratRecu.Mois

    printfn "\n"
    for y in listeDesRemboursement do
        printfn "soin: %A" y.NumSoin
        printfn "date: %A" y.DateSoin 
        printfn "montant: %f" y.Montant
        printfn "\n"
    
    printfn "total: %f" total