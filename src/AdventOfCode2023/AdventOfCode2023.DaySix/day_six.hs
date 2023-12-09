import System.IO
import System.Environment (getArgs)

main :: IO ()
main = do
    args <- getArgs
    content <- readFile (head args)
    let ls = map (tail . words) (lines content)
    print $ partOne ls
    print $ partTwo ls

partOne :: [[String]] -> Int
partOne ls = product $ map noOfWaysToWin (parseRacePairs ls)

partTwo :: [[String]] -> Int
partTwo ls = noOfWaysToWin (parseRacePair ls)

winningTimes :: (Int, Int) -> [Int]
winningTimes (t, n) = [k | k <- [1..t], (t-k)*k > n]

noOfWaysToWin :: (Int, Int) -> Int
noOfWaysToWin (a, b) = length $ winningTimes (a, b)

parseRacePairs :: [[String]] -> [(Int, Int)]
parseRacePairs (x1:x2:xs) = zip (map read x1) (map read x2)

parseRacePair :: [[String]] -> (Int, Int)
parseRacePair (x1:x2:xs) = (read (concat x1), read (concat x2))