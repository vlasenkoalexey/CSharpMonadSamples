-- maybe is a discriminated union that contains either wrapped 'a' value or Nothing_.
data Maybe_ a = Nothing_ | Just_ a 
  deriving (Show, Eq, Ord) -- Hasell shoud provide default implementations for common methods.

instance Monad Maybe_ where
   return x = Just_ x
   (>>=) maybe f = -- (>>==) stands for bind in Haskell
       case maybe of -- doing pattern matching on maybe
           Just_ x -> f x -- if maybe has value, apply function f to the wrapped value
           Nothing_ -> Nothing_ -- otherwise return Nothing_

instance Functor Maybe_ where
  fmap f (Just_ x) = Just_ (f x)
  fmap f Nothing_ = Nothing_

instance Applicative Maybe_ where
  pure = Just_
  (Just_ f) <*> (Just_ x) = Just_ (f x)
  _ <*> _ =  Nothing_