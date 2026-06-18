# Igrica Sing, Ducky!

3D video igra zasnovana na klasičnom konceptu "Simon Says" igrice, tj. ponavljanju obrazaca. Projekat je razvijen u **Unity 6** okruženju, dok su prilagođeni 3D modeli karaktera i njihove animacije kreirani od nule u programu **Blender**. Igrač ima zadatak da pažljivo posmatra redosled pevanja i svetlenja patkica, a zatim da precizno ponovi taj isti šablon kako bi osvojio poene (XP) i postavio najbolji rezultat (Highscore).

## Funkcionalnosti igre
* **Generisanje obrazaca:** Igra automatski kreira nasumične sekvence koje postaju sve duže i kompleksnije kako runde odmiču
* **Bonus događaj:** Dinamičko pojavljivanje bonus ptice koja preleće scenu tokom igre i pruža mogućnost igraču da osvoji dodatni XP
* **Praćenje korisničkog interfejsa (UI):** Prikaz trenutne runde, osvojenih poena i najboljeg rezultata u realnom vremenu 

---

## Tehnologije i alati
* **Game engine:** Unity 6
* **3D modelovanje i animacija:** Blender
* **Programski jezik:** C# (.NET)
* **UI Sistem:** Unity Canvas & TextMeshPro

---

## Arhitektura projekta i ključne skripte

Logika igre je pisana u C# skriptama:

* **`SimonSaysManager.cs`**: Glavna skripta koja vodi računa o logici rundi, računanju XP poena, validaciji igračevog klika i stanjima igre (Start, Play, Game Over).
* **`DuckController.cs`**: Skripta vezana za patkice, koja upravlja audio i vizuelnom reprodukcijom, pokreće animacije i efekte pevanja
* **`BirdSpawner.cs` & `BirdController.cs`**: Sistem zadužen za instanciranje i kontrolu putanje bonus ptice koja leti preko ekrana
* **`MenuScreen.cs`**: Upravljač korisničkim interfejsom koji kontroliše tranzicije između scena i interakciju sa menijem

---
## Pokretanje projekta lokalno

1. **Klonirajte repozitorijum** na svoj računar:
```bash
   git clone https://github.com/marijaag/VRSS-Unity-Sing-Ducky.git
```

2. **Otvorite Unity Hub** i kliknite na dugme **Add → Add project from disk**

3. **Pronađite klonirani folder** i potvrdite izbor

4. **Otvorite projekat** klikom na njega u Unity Hub listi — Unity će automatski učitati sve zavisnosti

5. U **Project prozoru**, idite do `Assets/Scenes/` i otvorite glavnu scenu (`MainMenu`)

6. Pritisnite dugme **Play** u Unity editoru i uživajte u igri!

---

## Način igre

1. Pokrenite igru i pritisnite **Start**
2. Posmatrajte redosled kojim patkice **pevaju**
3. Nakon što sekvenca završi, **kliknite na patkice istim redosledom**
4. Svaka ispravno ponovljena sekvenca donosi **XP poene** i prelazite u sledeću rundu
5. Ako primetite **bonus pticu** kako prelazi preko ekrana, kliknite na nju za **dodatni XP**!
6. Igra se završava pri prvoj grešci, tako da pokušajte da oborite svoj **Highscore**!
