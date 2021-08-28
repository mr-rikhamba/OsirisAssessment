using System;
using System.Collections.Generic;

namespace Movies.Logic.Models.NytModels
{


    public class NytTimesModel
    {
        public string status { get; set; }
        public string copyright { get; set; }
        public bool has_more { get; set; }
        public int num_results { get; set; }
        public List<ResultModel> results { get; set; }
    }

    public class LinkModel
    {
        public string type { get; set; }
        public string url { get; set; }
        public string suggested_link_text { get; set; }
    }

    public class MultimediaModel
    {
        public string type { get; set; }
        public string src { get; set; }
        public int height { get; set; }
        public int width { get; set; }
    }

    public class ResultModel
    {
        public string display_title { get; set; }
        public string mpaa_rating { get; set; }
        public int critics_pick { get; set; }
        public string byline { get; set; }
        public string headline { get; set; }
        public string summary_short { get; set; }
        public string publication_date { get; set; }
        public string opening_date { get; set; }
        public string date_updated { get; set; }
        public LinkModel link { get; set; }
        public MultimediaModel multimedia { get; set; }
    }


}
