using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VCardCreator.Models;

public class VCard
{




    public string Id { get; set; }
    public string Firstname { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Country { get; set; }
    public string City { get; set; }


    /* 
     BEGIN:VCARD
     VERSION:4.0
     N:Gump;Forrest;;Mr.;
     FN:Sheri Nom
     ORG:Sheri Nom Co.
     TITLE:Ultimate Warrior
     PHOTO;MEDIATYPE#image/gif:http://www.sherinnom.com/dir_photos/my_photo.gif
     TEL;TYPE#work,voice;VALUE#uri:tel:+1-111-555-1212
     TEL;TYPE#home,voice;VALUE#uri:tel:+1-404-555-1212
     ADR;TYPE#WORK;PREF#1;LABEL#"Normality\nBaytown\, LA 50514\nUnited States of America":;;100 Waters Edge;Baytown;LA;50514;United States of America
     ADR;TYPE#HOME;LABEL#"42 Plantation St.\nBaytown\, LA 30314\nUnited States of America":;;42 Plantation St.;Baytown;LA;30314;United States of America
     EMAIL:sherinnom@example.com
     REV:20080424T195243Z
     x-qq:21588891
     END:VCARD
    
     */



    public string ToVCardFormat()
    {

        return $"BEGIN:VCARD\n" +
               $"VERSION:3.0\n" +
               $"FN:{Firstname} {Surname}\n" +
               $"EMAIL:{Email}\n" +
               $"TEL:{Phone}\n" +
               $"ADR:;;{City};;{Country}\n" +
               $"END:VCARD";

    }

}


