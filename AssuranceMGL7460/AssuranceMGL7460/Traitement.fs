module Traitement

open System.IO
open FSharp.Data
open Soin
open Police
open Remboursement
open System
open System

type PoliceSoinsAssures = JsonProvider<"./ressources/polices.json">
type ReclamationSoinsAssures = JsonProvider<"./ressources/input1.json">



let leTraitement (input, output) =

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
                lesSoinAssure <- new SoinAssure(care.Soin, care.Pourcentage, Decimal.Parse(care.Limite.ToString()), Decimal.Parse(care.LimiteMensuelle.ToString())) :: lesSoinAssure

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
                    if(montantRemboursement < Decimal.Parse(itemsAssure.Limite.ToString()) && itemsAssure.Limite <> 0.0M) then 
                        lesSoinRembourse <- remboursement(itemsRecu.NumSoin.ToString(), itemsRecu.DateSoin, montantRemboursement) :: lesSoinRembourse

                    else
                        lesSoinRembourse <- remboursement(itemsRecu.NumSoin.ToString(), itemsRecu.DateSoin, Decimal.Parse(itemsAssure.Limite.ToString())) :: lesSoinRembourse

    let listeDesRemboursement  = lesSoinRembourse |> List.toArray

    let mutable total:decimal = 0.0M
    let calculMontantTotal =
        for remboursements in listeDesRemboursement do
            total <- remboursements.Montant + total

    RemboursementToJson (PoliceContratRecu.Dossier.ToString(), PoliceContratRecu.Mois.ToString(), listeDesRemboursement, total.ToString())


    