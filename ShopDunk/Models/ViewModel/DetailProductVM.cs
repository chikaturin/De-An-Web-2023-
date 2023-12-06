using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Web;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ShopDunk.Models.ViewModel
{
    public class DetailProductVMItem
    { 
        public Color _color {  get; set; }
        public Memory _mem { get; set; }
    }
    public class DetailProductVM
    {
        public List<DetailProductVMItem> _vms =new List<DetailProductVMItem>();
        public IEnumerable<DetailProductVMItem> Items
        {
            get { return _vms; }
        }
        [Key]
        public int ProDeID { get; set; }
        public int RemainQuantity { get; set; }
        public Nullable<int> SoldQuantity { get; set; }
        public Nullable<int> ViewQuantity { get; set; }
        public string ConnectNetwork { get; set; }

        //lấy từ bảng product
        public int ProID { get; set; }
        public string ProName { get; set; }
        public string ProImage { get; set; }
        public double Price { get; set; }
        public double PriceReduce { get; set; }

        public double DiscountPercent { get; set; }
        public System.DateTime CreatedDate { get; set; }

        //Category
        public int CatID { get; set; }
        public string NameCate { get; set; }

        //Color
        public int ColorID { get; set; }
        public string ColorName { get; set; }
        public string RGB { get; set; }

        //thon tin camera
        public int IDFCam { get; set; }
        public string ResolutionF { get; set; }

        public int IDRCam { get; set; }
        public string ResolutionR { get; set; }

        //Memory
        public int IDMem { get; set; }
        public string NameMem { get; set; }
        public int StorageMem { get; set; }

        //Battery
        public int BatID { get; set; }
        public int Storage { get; set; }
        public string NameBat { get; set; }


        //Screen
        public int IDScr { get; set; }
        public string NameScr { get; set; }
        public string Resolution { get; set; }
        public ICollection<DetailProductVM> RelatePro { get; set; }

    }
}