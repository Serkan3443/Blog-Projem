//Bu kodları yazamamaın amacı herhangi bir makaleye yorum yapmak istersem bu yorumum kaydolması için bu kodlamayı yapıyorum ve MakaleController da MakaleYorum methodunun içinde tanımlayıp daha sonra _YorumlarPartialView sayfasında input içinde çağırıyprum ve yorum yazma işlemimi bu şekilde olmuş oluyor
function YorumKaydet() {

    var makaleId = $("#makaleID").val();
    var yorum = $("#comment-message").val();
    //var kullaniciId = $("#").val();
    //var altYorumId = $("#").val();

    $.ajax({
        method: "post",
        url: "/Makale/MakaleYorum",
        data: { "makaleId": makaleId, "yorumIcerik":yorum }
    }).done(function (data) {
        if (data != null)
        {
            var yorumBasla = '<div class="comment d-flex mb - 4">';
            var yorumResim = '<div class="flex-shrink-0" ><div class="avatar avatar-sm rounded-circle"><img class="avatar-img img-fluid" src="~/template/assets/img/person-5.jpg" ></div>' + data.yorumResim+'</div>';
            var yorumAlani = '<div class="flex-grow-1 ms-2 ms-sm-3"><div class="comment-meta d-flex align-items-baseline" ><h6 class="me-2">' + data.Adi + '</h6><span class="text-muted">' +data.YorumTarihi +'</span></div><div class="comment-body">' + data.Yorum + '</div><div class="comment-replies bg-light p-3 mt-3 rounded"></div></div></div>';

            $("#yorumAlani").append(yorumBasla, yorumResim, yorumAlani);
            
        }


    }).fail(function () {



    });
};