using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComicDemoModels
{
    public class ComicBook
    {
        /// <summary>
        /// Unique ID of the issue.
        /// </summary>
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        /// <summary>
        /// The publish date printed on the cover of an issue.
        /// </summary>
        public DateTime? CoverDate { get; set; }

        /// <summary>
        /// Date the issue was added to Comic Vine.
        /// </summary>
        public DateTime? DateAdded { get; set; }

        /// <summary>
        /// Date the issue was last updated on Comic Vine.
        /// </summary>
        public DateTime? DateLastUpdated { get; set; }

        /// <summary>
        /// Brief summary of the issue.	
        /// </summary>
        public string Deck { get; set; }

        /// <summary>
        /// Description of the issue.	
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Main image of the issue.			
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Main image of the issue.			
        /// </summary>
        public int? IssueNumber { get; set; }

        /// <summary>
        /// Main image of the issue.			
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL pointing to the issue on Giant Bomb. (renamed from site_detail_url for redundancy)	
        /// </summary>
        public string IssueDetailURL { get; set; }

        /// <summary>
        /// The volume this issue is a part of.			
        /// </summary>
        public int? Volume { get; set; }

        /// <summary>
        /// URL pointing to the volume on Giant Bomb. (renamed from site_detail_url for redundancy)				
        /// </summary>
        public string VolumeDetailURL { get; set; }

        /// <summary>
        /// Note added by our local user				
        /// </summary>
        public string UserNote { get; set; }


    }

}



