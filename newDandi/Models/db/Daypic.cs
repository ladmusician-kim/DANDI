//------------------------------------------------------------------------------
// <auto-generated>
//    이 코드는 템플릿에서 생성되었습니다.
//
//    이 파일을 수동으로 변경하면 응용 프로그램에 예기치 않은 동작이 발생할 수 있습니다.
//    코드가 다시 생성되면 이 파일에 대한 수동 변경 사항을 덮어씁니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace newDandi.Models.db
{
    using System;
    using System.Collections.Generic;
    
    public partial class Daypic
    {
        public int C_id { get; set; }
        public byte[] pic { get; set; }
        public System.DateTime created { get; set; }
        public Nullable<System.DateTime> updated { get; set; }
        public int for_projectid { get; set; }
        public bool isdeprecated { get; set; }
        public string title { get; set; }
        public string content { get; set; }
    
        public virtual Project Project { get; set; }
    }
}
