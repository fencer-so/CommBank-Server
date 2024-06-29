#!/bin/bash
for i in {1..100}
do
  echo "Hack the contribution graph $i" >> hack.txt
  git add hack.txt
  git commit -m "Hack commit $i"
  git push origin main
done
