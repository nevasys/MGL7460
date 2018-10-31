module JSONHash

open System.IO
open FSharp.Data
open System.Text.RegularExpressions
open Traitement


type fichierJSON (fichierInput, fichierOutput) =
    member this.fichierInput = fichierInput
    member this.fichierOutput = fichierOutput

    //let fichierJSON = "./ressources/input1.json";


//type Reclamation = JsonProvider<"./ressources/input1.json">

let lireFichierReclamation(fichierInput, fichierOuput) =

   
    //let fichierJson = "./ressources/" + fichierInput

    //let items = Reclamation.Parse(File.ReadAllText("./ressources/input1.json"))

        leTraitement(fichierInput, fichierOuput)

