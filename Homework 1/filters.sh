marijat@dasmarice:/tmp/VirtualBox Dropped Files/2020-11-13T20:38:18.195484000Z/dizajn$ awk -F , '$6 == "macedonian" { print }' test2.csv > input1.csv
marijat@dasmarice:/tmp/VirtualBox Dropped Files/2020-11-13T20:38:18.195484000Z/dizajn$ awk -F , '$6 == "Macedonian" { print }' test2.csv >> input1.csv
marijat@dasmarice:/tmp/VirtualBox Dropped Files/2020-11-13T20:38:18.195484000Z/dizajn$ awk -F , '$6 == "regional" { print }' test2.csv >> input1.csv
marijat@dasmarice:/tmp/VirtualBox Dropped Files/2020-11-13T20:38:18.195484000Z/dizajn$ awk -F , '$6 == "grill" { print }' test2.csv >> input1.csv
marijat@dasmarice:/tmp/VirtualBox Dropped Files/2020-11-13T20:38:18.195484000Z/dizajn$ awk -F , '$6 == "balkan" { print }' test2.csv >> input1.csv
