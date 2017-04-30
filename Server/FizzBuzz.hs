module FizzBuzz where

fizzBuzz :: Integer -> String
fizzBuzz n | fizzBuzzCombo n == "" = show n
           | otherwise             = show n ++ " becomes " ++ fizzBuzzCombo n
  where
    fizzBuzzCombo n = fizz n ++ buzz n
    fizz = anyzz "Fizz" 3
    buzz = anyzz "Buzz" 5
    anyzz word factor n | n `mod` factor == 0 = word
                        | otherwise           = ""
