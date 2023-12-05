open System
open System.IO

type CardType = 
    { CardNumber : int
      MatchingNumbers : int list
      Count : int }

let findMatchingNumbers (winningNumbers: int list) (guessedNumbers: int list) =
    let rec findMatchingNumbers' (winningNumbers: int list) (guessedNumbers: int list) (matchingNumbers: int list) =
        match winningNumbers with
        | [] -> matchingNumbers
        | x::xs -> 
            if List.contains x guessedNumbers then
                findMatchingNumbers' xs (List.filter (fun k -> k <> x) guessedNumbers) (x::matchingNumbers)
            else
                findMatchingNumbers' xs guessedNumbers matchingNumbers
    findMatchingNumbers' winningNumbers guessedNumbers []

let addNextNCardCopies (n: int) (copyCount: int) (cards: CardType list) =
    let rec addNextNCardCopies' (n: int) (copyCount: int) (cards: CardType list) (copies: CardType list) =
        match (n, cards) with
        | _, [] -> copies
        | k, x::xs ->
            if k > 0 then
                addNextNCardCopies' (k - 1) copyCount xs (List.append copies [{x with Count = x.Count + copyCount}])
            else
                addNextNCardCopies' k copyCount xs (List.append copies [x])
    addNextNCardCopies' n copyCount cards []

let countCards (cards: CardType list) =
    let rec countCards' (cards: CardType list) (acc: int) =
        match cards with
        | [] -> acc
        | x::xs ->
            let copiesToAdd = x.Count
            let nextN = x.MatchingNumbers.Length
            let copies = addNextNCardCopies nextN copiesToAdd xs
            countCards' copies (acc + x.Count)
    countCards' cards 0
    
let parseCardNumber (cardNumber: string) =
    let parts = cardNumber.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
    parts |> Array.last |> int
    
let parseListOfNumbers (numbers: string) =
    numbers.Split([|' '|], StringSplitOptions.RemoveEmptyEntries) |> Array.map int |> Array.toList
    
let parseLine (line: string) =
    let parts1 = line.Split([|':'|], StringSplitOptions.TrimEntries)
    let cardNumber = parseCardNumber (parts1 |> Array.head)
    let numbers = parts1 |> Array.last    
    let parts2 = numbers.Split([|'|'|], StringSplitOptions.TrimEntries)
    let winningNumbers = parseListOfNumbers (parts2 |> Array.head)
    let guessedNumbers = parseListOfNumbers (parts2 |> Array.last)
    let matchingNumbers = findMatchingNumbers winningNumbers guessedNumbers
    { CardNumber = cardNumber; MatchingNumbers = matchingNumbers; Count = 1 }
    
let parseLines (lines: string list) =
    lines |> List.map parseLine
    
let cardPoints (card: CardType) =
    let points = pown 2 (card.MatchingNumbers.Length - 1)
    points

let sumCardPoints (cards: CardType list) =
    cards |> List.map cardPoints |> List.sum

let partOne (lines: string list) =
    let totalPoints = parseLines lines |> sumCardPoints
    totalPoints

let partTwo (lines: string list) =
    let totalCards = parseLines lines |> countCards
    totalCards

let testLines = [|
     "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53";
    "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19";
    "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1";
    "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83";
    "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36";
    "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11" |] |> Array.toList

let inputFile = "input.txt"
let lines = File.ReadAllLines inputFile |> Array.toList

let partOneSuccess = partOne testLines = 13
let totalPoints = partOne lines
printfn "Part one passed: %b" partOneSuccess
printfn "Total points: %d" totalPoints

let partTwoSuccess = partTwo testLines = 30
let totalCards = partTwo lines
printfn "Part two passed: %b" partTwoSuccess
printfn "Total scratchcards: %d" totalCards
