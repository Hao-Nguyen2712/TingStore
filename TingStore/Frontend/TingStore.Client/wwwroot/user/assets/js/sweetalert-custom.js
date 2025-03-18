function showSuccessAlert(message, title = "Thành công!") {
    Swal.fire({
        icon: 'success',
        title: title,
        text: message,
        timer: 3000,
        timerProgressBar: true, 
        showConfirmButton: false,
        customClass: {
            popup: 'swal-rounded'
        }
    });
}

function showErrorAlert(message, title = "Lỗi!") {
    Swal.fire({
        icon: 'error',
        title: title,
        text: message,
        timer: 3000,
        timerProgressBar: true,
        showConfirmButton: false,
        customClass: {
            popup: 'swal-rounded'
        }
    });
}

function showWarningAlert(message, title = "Cảnh báo!") {
    Swal.fire({
        icon: 'warning',
        title: title,
        text: message,
        timer: 3000,
        timerProgressBar: true,
        showConfirmButton: false,
        customClass: {
            popup: 'swal-rounded'
        }
    });
}
