using System.Reflection;
using System.Runtime.InteropServices;

// Общие сведения об этой сборке предоставляются следующим набором 
// атрибутов. Отредактируйте значения этих атрибутов, чтобы изменить
// общие сведения об этой сборке.
[assembly: AssemblyTitle("TMGmod")]
[assembly: AssemblyDescription("Grandful Management: Shotguns")]
//[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("OnGoTeam")]
[assembly: AssemblyProduct("TMGmod")]
[assembly: AssemblyCopyright("Copyright © 2020 OGT")]
//[assembly: AssemblyTrademark("")]
//[assembly: AssemblyCulture("")]

// Установка значения False в параметре ComVisible делает типы в этой сборке невидимыми 
// для COM-компонентов.  Если необходим доступ к типу в этой сборке из 
// COM, следует установить атрибут ComVisible в TRUE для этого типа.
[assembly: ComVisible(false)]

// Следующий GUID служит для идентификации библиотеки типов, если этот проект будет видимым для COM
[assembly: Guid("190a2e48-c24a-4075-89ae-2e60aea4c0f1")]

// Сведения о версии сборки состоят из следующих четырех значений:
//
//      Основной номер версии
//      Дополнительный номер версии 
//   Номер сборки
//      Редакция
//

// proposed system:
// major                            1
// minor*100 + major milestone    203
// minor milestone                  4
// patch (revision)                 5

#if WORKSHOP
[assembly: AssemblyVersion("1.1.6.1")]
#else
[assembly: AssemblyVersion("1.102.*")]
#endif
