using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportExportNAVObjects
{
    class NAVObjectFilter
    {
        private const string Separator = ";";
        private const string TypeConst = "Type=";
        private const string IDConst = "ID=";
        private const string NameConst = "Name=";
        private const string ModifiedConst = "Modified=";
        private const string CompiledConst = "Compiled=";
        private const string DateConst = "Date=";
        private const string TimeConst = "Time=";
        private const string VersionListConst = "Version List=";
        private const string CaptionConst = "Caption=";
        private const string LockedConst = "Locked=";
        private const string LockedByConst = "Locked By=";

        private string TypeFilter;
        private string IDFilter;
        private string NameFilter;
        private string ModifiedFilter;
        private string CompiledFilter;
        private string DateFilter;
        private string TimeFilter;
        private string VersionListFilter;
        private string CaptionFilter;
        private string LockedFilter;
        private string LockedByFilter;

        public NAVObjectFilter(string typeFilter = "", string idFilter = "", string nameFilter = "", string modifiedFilter = "",
            string compiledFilter = "", string dateFilter = "", string timeFilter = "", string versionListFilter = "",
            string captionFilter = "", string lockedFilter = "", string lockedByFilter = "")
        {
            TypeFilter = typeFilter;
            IDFilter = idFilter;
            NameFilter = nameFilter;
            ModifiedFilter = modifiedFilter;
            CompiledFilter = compiledFilter;
            DateFilter = dateFilter;
            TimeFilter = timeFilter;
            VersionListFilter = versionListFilter;
            CaptionFilter = captionFilter;
            LockedFilter = lockedFilter;
            LockedByFilter = lockedByFilter;
        }

        public static string NonBlankValue(string sourceString, string constString)
        {
            return String.IsNullOrEmpty(sourceString) ? String.Empty : String.Concat(constString, sourceString);
        }

        private void AddElementToList(ref List<string> stringList, string element)
        {
            if (!String.IsNullOrEmpty(element))
            {
                stringList.Add(element);
            }
        }

        private string[] CreateFilterArray()
        {
            List<string> filterList = new List<string>();

            AddElementToList(ref filterList, NonBlankValue(TypeFilter, TypeConst));
            AddElementToList(ref filterList, NonBlankValue(IDFilter, IDConst));
            AddElementToList(ref filterList, NonBlankValue(NameFilter, NameConst));
            AddElementToList(ref filterList, NonBlankValue(ModifiedFilter, ModifiedConst));
            AddElementToList(ref filterList, NonBlankValue(CompiledFilter, CompiledConst));
            AddElementToList(ref filterList, NonBlankValue(DateFilter, DateConst));
            AddElementToList(ref filterList, NonBlankValue(TimeFilter, TimeConst));
            AddElementToList(ref filterList, NonBlankValue(VersionListFilter, VersionListConst));
            AddElementToList(ref filterList, NonBlankValue(CaptionFilter, CaptionConst));
            AddElementToList(ref filterList, NonBlankValue(LockedFilter, LockedConst));
            AddElementToList(ref filterList, NonBlankValue(LockedByFilter, LockedByConst));
            
            return filterList.ToArray();
        }

        public string GetFilterString()
        {
            return String.Join(Separator, CreateFilterArray());
        }
    }
}
