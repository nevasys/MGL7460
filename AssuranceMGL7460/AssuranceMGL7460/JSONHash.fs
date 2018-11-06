module JSONHash

open System.IO
open FSharp.Data
open System.Text.RegularExpressions
open Traitement


type fichierJSON (fichierInput, fichierOutput) =
    member this.fichierInput = fichierInput
    member this.fichierOutput = fichierOutput

let lireFichierReclamation(fichierInput, fichierOuput) =

        leTraitement(fichierInput, fichierOuput)

