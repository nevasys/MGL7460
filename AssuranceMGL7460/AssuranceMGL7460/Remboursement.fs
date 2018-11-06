module Remboursement

open System.IO
open FSharp.Data
open System.Runtime.Serialization.Json
open System.Runtime.Serialization
open System.Runtime.Serialization.Formatters.Binary
open System.Xml
open System
open Xunit


//type Remboursement (numSoin, dateSoin, montant : decimal) =
    //member this.NumSoin = numSoin
    //member this.DateSoin = dateSoin
    //member this.Montant = montant


type Remboursement  =
    {
        [<field : DataMember>]
        NumSoin : string;

        [<field : DataMember>]
        DateSoin : DateTime;

        [<field : DataMember>]
        Montant : decimal;
    }

let remboursement (numSoin, dateSoin, montant) = {NumSoin = numSoin; DateSoin = dateSoin; Montant = montant} 

let RemboursementToJson (dossier, mois, listeDesRemboursement : Remboursement[], total) =
    
    File.AppendAllText("./ressources/output_reclamation.json", "{" + Environment.NewLine)
    File.AppendAllText("./ressources/output_reclamation.json", "  \"dossier\": \"" + dossier + "\"" + Environment.NewLine)
    File.AppendAllText("./ressources/output_reclamation.json", "  \"mois\": \"" + mois + "\"" + Environment.NewLine)
    File.AppendAllText("./ressources/output_reclamation.json", "  \"reclamations\": [" + Environment.NewLine)
    for item in listeDesRemboursement do
        File.AppendAllText("./ressources/output_reclamation.json", "    {" + Environment.NewLine)
        File.AppendAllText("./ressources/output_reclamation.json", "    \"soin\": \"" + item.NumSoin + "\"" + Environment.NewLine)
        File.AppendAllText("./ressources/output_reclamation.json", "    \"date\": \"" + item.DateSoin.ToString() + "\"" + Environment.NewLine)
        File.AppendAllText("./ressources/output_reclamation.json", "    \"montant\": \"" + item.Montant.ToString() + "$\"" + Environment.NewLine)
        File.AppendAllText("./ressources/output_reclamation.json", "    }," + Environment.NewLine)
    File.AppendAllText("./ressources/output_reclamation.json", "  ]," + Environment.NewLine)
    File.AppendAllText("./ressources/output_reclamation.json", "  \"total\": \"" + total + "$\"" + Environment.NewLine)
    File.AppendAllText("./ressources/output_reclamation.json", "}")
