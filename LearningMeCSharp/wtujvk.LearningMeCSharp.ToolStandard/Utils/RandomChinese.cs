using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.ToolStandard.Utils
{
    /// <summary>
    /// 中文随机数
    /// </summary>
    public class RandomChinese
    {

        public RandomChinese()
        {
        }
        /// <summary>
        /// 生成随机汉字
        /// </summary>
        /// <param name="strlength">个数</param>
        /// <param name="def"></param>
        /// <returns></returns>
        public string GetRandomChinese(ushort strlength,out string def)
        {
            // 获取GB2312编码页（表） 
            Encoding gb = Encoding.GetEncoding("gb2312");
           // object[] bytes = this.CreateRegionCode(strlength);
            var bytesLst = CreateRegionCode(strlength);
            StringBuilder sb = new StringBuilder(strlength);
            StringBuilder sb2=new StringBuilder(strlength);
            //for (int i = 0; i < strlength; i++)
            //{
            //   // string temp = gb.GetString((byte[]) Convert.ChangeType(bytes[i], typeof(byte[])));
            //    string temp = gb.GetString(bytesLst.ElementAt(i));
            //    sb.Append(temp);
            //}
            //foreach (var item in bytesLst)
            //{
            //    sb.Append(gb.GetString(item));
            //}
            bytesLst.ToList().ForEach(b=>
            {
                sb.Append(gb.GetString(b));
                sb2.Append(b);
            });
            def = sb2.ToString();
            return sb.ToString();
        }
        /** 
        此函数在汉字编码范围内随机创建含两个元素的十六进制字节数组，每个字节数组代表一个汉字，并将 
        四个字节数组存储在object数组中。 
        参数：strlength，代表需要产生的汉字个数 
        **/
        private IEnumerable<byte[]> CreateRegionCode(ushort strlength)
        {
            //定义一个字符串数组储存汉字编码的组成元素 
            string[] rBase = new String[16]
                {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f"};
            Random rnd = new Random();
            //定义一个object数组用来 
            //object[] bytes = new object[strlength];
            var byteslst=new List<byte[]>(strlength);
            /**
             每循环一次产生一个含两个元素的十六进制字节数组，并将其放入bytes数组中 
             每个汉字有四个区位码组成 
             区位码第1位和区位码第2位作为字节数组第一个元素 
             区位码第3位和区位码第4位作为字节数组第二个元素 
            **/
            for (int i = 0; i < strlength; i++)
            {
                //区位码第1位 
                int r1 = rnd.Next(11, 14);
                string str_r1 = rBase[r1].Trim();
                //区位码第2位 
                rnd = new Random(r1 * unchecked((int) DateTime.Now.Ticks) + i); // 更换随机数发生器的 种子避免产生重复值 
                int r2 = rnd.Next(0, r1 == 13 ? 7 : 16);
                string str_r2 = rBase[r2].Trim();
                //区位码第3位 
                rnd = new Random(r2 * unchecked((int) DateTime.Now.Ticks) + i);
                int r3 = rnd.Next(10, 16);
                string str_r3 = rBase[r3].Trim();
                //区位码第4位 
                rnd = new Random(r3 * unchecked((int) DateTime.Now.Ticks) + i);
                int r4;
                if (r3 == 10)
                {
                    r4 = rnd.Next(1, 16);
                }
                else if (r3 == 15)
                {
                    r4 = rnd.Next(0, 15);
                }
                else
                {
                    r4 = rnd.Next(0, 16);
                }
                string str_r4 = rBase[r4].Trim();
                // 定义两个字节变量存储产生的随机汉字区位码 
                byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
                byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
                // 将两个字节变量存储在字节数组中 
                byte[] str_r = new byte[] {byte1, byte2};
                // 将产生的一个汉字的字节数组放入object数组中 
               // bytes.SetValue(str_r, i);
                byteslst.Add(str_r);
            }
            return byteslst;
        }

        /// <summary>
        /// 随机产生常用汉字
        /// </summary>
        /// <param name="count">要产生汉字的个数</param>
        /// <returns>常用汉字</returns>
        public static string GenerateChineseWord(ushort count)
        {
            System.Random rm = new System.Random();
            Encoding gb = Encoding.GetEncoding("gb2312");
            StringBuilder sb = new StringBuilder(count);
            for (int i = 0; i < count; i++)
            {
                 int regionCode = rm.Next(16, 56);  // 获取区码(常用汉字的区码范围为16-55)
                // 获取位码(位码范围为1-94 由于55区的90,91,92,93,94为空,故将其排除)
                int positionCode = rm.Next(1, regionCode == 55 ? 90 : 95);
                // 转换区位码为机内码
                var regionCode_Machine = (byte)((regionCode + 160)%byte.MaxValue);// 160即为十六进制的20H+80H=A0H
                var positionCode_Machine = (byte)((positionCode + 160) % byte.MaxValue);// 160即为十六进制的20H+80H=A0H
                // 转换为汉字
                byte[] bytes = {regionCode_Machine,positionCode_Machine };
                sb.Append(gb.GetString(bytes));
            }
            return sb.ToString();
        }

        private static string firstName = @"赵,钱,孙,李,周,吴,郑,王,冯,陈,褚,卫,蒋,沈,韩,杨,朱,秦,尤,许,何,吕,施,张,孔,曹,严,华,金,魏,陶,姜,戚,谢,邹,喻,柏,水,窦,章,云,苏,潘,葛,奚,范,彭,郎,鲁,韦,昌,马,苗,凤,花,方,俞,任,袁,柳,丰,鲍,史,唐,费,廉,岑,薛,雷,贺,倪,汤,滕,殷,罗,毕,郝,邬,安,常,乐,于,时,傅,皮,卞,齐,康,伍,余,元,卜,顾,孟,平,黄,和,穆,萧,尹,姚,邵,湛,汪,祁,毛,禹,狄,米,贝,明,臧,计,伏,成,戴,谈,宋,茅,庞,熊,纪,舒,屈,项,祝,董,梁,杜,阮,蓝,闵,席,季,麻,强,贾,路,娄,危,江,童,颜,郭,梅,盛,林,刁,钟,徐,丘,骆,高,夏,蔡,田,樊,胡,凌,霍,虞,万,支,柯,昝,管,卢,莫,经,房,裘,缪,干,解,应,宗,丁,宣,贲,邓,郁,单,杭,洪,包,诸,左,石,崔,吉,钮,龚,程,嵇,邢,滑,裴,陆,荣,翁,荀,羊,於,惠,甄,麴,家,封,芮,羿,储,靳,汲,邴,糜,松,井,段,富,巫,乌,焦,巴,弓,牧,隗,山,谷,车,侯,宓,蓬,全,郗,班,仰,秋,仲,伊,宫,宁,仇,栾,暴,甘,钭,厉,戌,祖,武,符,刘,景,詹,束,龙,叶,幸,司,韶,郜,黎,蓟,薄,印,宿,白,怀,蒲,邰,从,鄂,索,咸,籍,赖,卓,蔺,屠,蒙,池,乔,阴,郁,胥,能,苍,双,闻,莘,党,翟,谭,贡,劳,逢,姬,申,扶,堵,冉,宰,郦,雍,郤,璩,桑,桂,濮,牛,寿,通,边,扈,燕,冀,郏,浦,尚,农,温,别,庄,晏,柴,瞿,阎,充,慕,连,茹,习,宦,艾,鱼,容,向,古,易,慎,戈,廖,庾,终,暨,居,衡,步,都,耿,满,弘,匡,国,文,寇,广,禄,阙,东,欧,殳,沃,利,蔚,越,菱,隆,师,巩,厍,聂,晃,勾,敖,融,冷,訾,辛,阚,那,简,饶,空,曾,毋,沙,乜,养,鞠,须,丰,巢,关,蒯,相,查,后,荆,红,游,竺,权,逯,盖,益,桓,公,司马,上官,欧阳,夏侯,诸葛,闻人,东方,赫连,皇甫,尉迟,公羊,澹台,公冶,宗政,濮阳,淳于,单于,太叔,申屠,公孙,仲孙,轩辕,令狐,钟离,宇文,长孙,慕容,司徒,司空,西门,北堂,南宫,百里";
        private static string lastName = @"努,迪,立,林,维,吐,丽,新,涛,米,亚,克,湘,明,白,玉,代,孜,霖,霞,加,永,卿,约,小,刚,光,峰,春,基,木,国,娜,晓,兰,阿,伟,英,元,音,拉,亮,玲,兴,成,尔,远,东,华,旭,吉,高,翠,莉,云,军,荣,柱,科,生,昊,耀,汤,胜,坚,仁,学,延,庆,初,杰,宪,雄,久,培,祥,梅,顺,西,库,康,温,校,信,志,图,艾,赛,潘,多,振,继,福,柯,雷,田,也,勇,乾,其,买,姚,杜,关,陈,静,宁,马,德,水,梦,晶,精,瑶,朗,语,日,月,星,河,飘,渺,空,如,萍,棕,影,南,北,妹,毅,俊,强,平保,文,辉,力,健,世,广,义,良,海,山,波,贵,龙,全,才,发,武,利,清,飞,彬,富,子,昌,星,光,天,达,安,岩,中,茂,进,有,和,彪,博,诚,先,敬,震,壮,会,思,群,豪,心,邦,承,乐,绍,功,松,善,厚,磊,民,友,裕,哲,江,超,浩,政,谦,亨,奇,固,之,轮,翰,伯,宏,言,若,鸣,朋,斌,梁,栋,启,伦,翔,鹏,泽,晨,辰,士,以,建,家,致,树,炎,行,时,泰,盛,琛,钧,冠,策,腾,楠,榕,风,航,弘,秀,娟,慧,巧,美,淑,惠,珠,雅,芝,红,娥,芬,芳,燕,彩,菊,凤,洁,琳,素,莲,真,环,雪,爱,香,莺,媛,艳,瑞,凡,佳,嘉,琼,勤,珍,贞,桂,娣,叶,璧,璐,娅,琦,妍,茜,秋,珊,莎,锦,黛,青,倩,婷,姣,婉,娴,瑾,颖,露,怡,婵,雁,蓓,纨,仪,荷,丹,蓉,眉,君,琴,蕊,薇,菁,岚,苑,婕,馨,瑗,琰,韵,融,园,艺,咏,聪,澜,纯,毓,悦,昭,冰,爽,琬,茗,羽,希,欣,育,滢,馥,筠,柔,竹,霭,凝,鱼,欢,霄,枫,芸,菲,寒,伊,宜,可,姬,舒,荔,枝,墨";
        /// <summary>
        /// 获取随机姓名
        /// </summary>
        /// <returns></returns>
        public static string GetRandChineseName()
        {
            Random ran = new Random();
            var firstarry = firstName.Split(new string[] {","}, StringSplitOptions.RemoveEmptyEntries);
            var lastarry = lastName.Split(new string[] {","}, StringSplitOptions.RemoveEmptyEntries);
            return $"{firstarry[ran.Next(firstarry.Length)]}{lastarry[ran.Next(lastarry.Length)]}";
        }
    }
}
