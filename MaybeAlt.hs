data Maybe_ a = Nothing_ | Just_ a
  deriving (Show, Eq, Ord)

instance Functor Maybe_ where
  fmap f (Just_ x) = Just_ (f x)
  fmap f Nothing_ = Nothing_

instance Applicative Maybe_ where
  pure = Just_
  (Just_ f) <*> (Just_ x) = Just_ (f x)
  _ <*> _ =  Nothing_

instance Monad Maybe_ where
   Nothing_  >>= _ = Nothing_ -- >>== stands for bind
   (Just_ x) >>= f =  sf x -- it is declared here as infix function
   return x = Just_ x
