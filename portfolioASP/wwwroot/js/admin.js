function Delete(url, saTitle, saText, saConfirmButtonText, saCancelButtonText) {
    Swal.fire({
        title: saTitle,
        text: saText,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d9534f",
        cancelButtonColor: "#343a40",
        confirmButtonText: saConfirmButtonText,
        cancelButtonText: saCancelButtonText,
        background: '#151a1c'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    location.reload();
                }
            })
        }
    });
}