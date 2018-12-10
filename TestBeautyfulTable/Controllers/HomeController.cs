using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeautifulTable;
using TestMyTable.Models;

namespace TestBeautyfulTable.Controllers
{
    public class HomeController : Controller
    {
      
        public ActionResult TestTable()
        {
            List<SampelData> sampelDatas = MakeSampleData();
            //MyTable.FontColor("white", "white", "");
            return View(sampelDatas);
        }

        public ActionResult TestTableRtl()
        {
            List<SampelData> sampelDatas = MakeSampleData();
            //MyTable.FontColor("white", "white", "");
            return View(sampelDatas);
        }

        public List<SampelData> MakeSampleData()
        {


            var sampelDatas = new List<SampelData>();
            for (int i = 1; i < 107; i++)
            {
                sampelDatas.Add(new SampelData()
                {
                    Id = i,
                    Name = "Milad" + i,
                    Family = "Safoory",
                    Email = "m@gmail.com",
                    LastSeen = DateTime.Now,
                    Statue = "online",
                    About = "hjhsdhsadhsd",
                    Discription = "ixhchjxgc"
                });
            }

            sampelDatas.Add(new SampelData()
            {
                Id = 107,
                Name = "Milad" + 66,
                Family = "Safoory",
                Email = "m@gmail.com",
                LastSeen = DateTime.Now,
                Statue = "online",
                About = "hjhsdhsadhsd",
                Discription = "ixhchjxgc"
            });
            sampelDatas.Add(new SampelData()
            {
                Id = 108,
                Name = "Milad" + 66,
                Family = "Safoory",
                Email = "m@gmail.com",
                LastSeen = DateTime.Now,
                Statue = "offline",
                About = "hjhsdhsadhsd",
                Discription = "ixhchjxgc"
            });
            sampelDatas.Add(new SampelData()
            {
                Id = 109,
                Name = "Milad" + 65,
                Family = "Safoory",
                Email = "m@gmail.com",
                LastSeen = DateTime.Now,
                Statue = "offline",
                About = "hjhsdhsadhsd",
                Discription = "ixhchjxgc"
            });
            ViewBag.currentPage = 1;
            ViewBag.countPage = 3;
            ViewBag.listIndex = 1;
            return sampelDatas;
        }


    }
}