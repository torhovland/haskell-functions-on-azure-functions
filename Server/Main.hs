module Main where

import System.Environment
import FizzBuzz
 
main :: IO ()
main = getArgs >>= putStrLn . fizzBuzz . read . head