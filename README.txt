Ten projekt stworzony zosta� w 24 godziny podczas Hackathonu HackSilesia #3: For Better Communities na zlecenie pana Patryka Bia�asa z Katowickiego Alarmu Smogowego przy u�yciu programu Microsoft Visual Studio 2017.

Autorzy:
Wronika Ziemianek (Grafika/Product Management)
Pawe� Szydziak (Backend)
Jakub Holewik (Frontend)

Wa�ne informacje dotycz�ce deployowania:

Aplikacja ma dwie wersje: admin i user.
Wersja user jest przeznaczona do dystrybucji dla u�ytkownik�w.
Wersja admin powsta�a aby umie�ci� j� na serwerze Katowickiego Alarmu Smogowego, aby mog�a sobie chodzi� przez 24/7 i dwa razy dziennie wysy�a� emaile.

Przy kompilacji domy�lna jest wersja user je�li chcemy skompliowa� wersj� admin nale�y odkomentowa� fragment kodu zajmuj�cy si� wysy�aniem emaili (znajduje si� on w pliku MainWindow.xaml.cs, na ko�cu konstruktora), a tak�e umie�ci� dwa pliki tekstowe w folderze z exekiem:
Mail.txt - zawieraj�cy list� adres�w email do kt�rych dostarczany ma by� email wysy�any przez aplikacj� (oddzielone enterami)
Credentials.txt - zawieraj�cy w pierwszej linic� nazw� u�ytkownika, a w drugiej has�o do adresu email z kt�rego emaile maj� by� wysy�ane (kod obs�uguje tylko adresy Gmail).