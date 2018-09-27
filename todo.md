Angels Management
==

# Todo
- [ ] Hasło na wejście aplikacji
    - [ ] Możliwość zmiany hasła

## Monika
- [x] Konstrukcja bazy
- [x] Dodawanie:
    - [x] rodziców
    - [x] podopiecznych
    - [x] wolontariuszy
    - [x] walidacja
- [x] Wyświetlanie:
    - [x] wolontariuszy 
    - [x] rodziców 
    - [x] podopiecznych 
- [x] Edytowanie wolo, podopiecznych, rodziców
- [x] Usuwanie:
    - [x] wolontariuszy
    - [x] podopiecznych
    - [x] rodziców

## Mikołaj
- [ ] Dodawanie miast

# Bugi

# Propozycje 
:grey_question: Po kliknięciu prawym przyciskiem myszy na wierszu     otwierałoby się spersonalizowane dla odpowiedniej tabeli menu kontekstowe  (np. `Pokaż podopiecznych` na ekranie `Wolontariusze` itp). Po kliknięciu otwierałoby się nowe okno z wynikami. Wystarczyłoby zaprojektować jedno okno, a zapytania byłyby tworzone na podstawie kontekstu 

:grey_question: Logowanie zamiast globalnego hasła. Użytkownik zwykły może tylko przeglądać, Administrator może dodawać/usuwać

:grey_question: Możliwość dodawania/usuwania kolumn przez użytkownika (np. załóżmy, że będą chcieli, żeby wolontariusze podawali zainteresowania czy coś tam). Najłatwiej chyba dokonywać małoinwazyjnego `ALTER TABLE` po zmianach. Można też bardziej "elegancko" (?) czyli podzielić na 2 tabele np. `VolunteersColumns` i `VolunteersData`, tylko trzeba by rozwiązać problem z typami danych

:grey_question: Ustawienia, możliwość zmiany hasła itp.

# Problemy

# Inne
## O Markdownie
https://help.github.com/articles/basic-writing-and-formatting-syntax/
https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet
