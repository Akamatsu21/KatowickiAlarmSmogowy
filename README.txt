Ten projekt stworzony zosta³ w 24 godziny podczas Hackathonu HackSilesia #3: For Better Communities na zlecenie pana Patryka Bia³asa z Katowickiego Alarmu Smogowego przy u¿yciu programu Microsoft Visual Studio 2017.

Autorzy:
Wronika Ziemianek (Grafika/Product Management)
Pawe³ Szydziak (Backend)
Jakub Holewik (Frontend)

Wa¿ne informacje dotycz¹ce deployowania:

Aplikacja ma dwie wersje: admin i user.
Wersja user jest przeznaczona do dystrybucji dla u¿ytkowników.
Wersja admin powsta³a aby umieœciæ j¹ na serwerze Katowickiego Alarmu Smogowego, aby mog³a sobie chodziæ przez 24/7 i dwa razy dziennie wysy³aæ emaile.

Przy kompilacji domyœlna jest wersja user jeœli chcemy skompliowaæ wersjê admin nale¿y odkomentowaæ fragment kodu zajmuj¹cy siê wysy³aniem emaili (znajduje siê on w pliku MainWindow.xaml.cs, na koñcu konstruktora), a tak¿e umieœciæ dwa pliki tekstowe w folderze z exekiem:
Mail.txt - zawieraj¹cy listê adresów email do których dostarczany ma byæ email wysy³any przez aplikacjê (oddzielone enterami)
Credentials.txt - zawieraj¹cy w pierwszej linicê nazwê u¿ytkownika, a w drugiej has³o do adresu email z którego emaile maj¹ byæ wysy³ane (kod obs³uguje tylko adresy Gmail).