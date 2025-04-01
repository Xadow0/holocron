import { toast } from 'react-toastify'

export const showSuccessToast = (message: string) => {
  toast.success(message, {
    position: 'top-right',
    autoClose: 3000,
    hideProgressBar: false,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
  })
}

export const showErrorToast = (message: string) => {
  toast.error(message, {
    position: 'top-right',
    autoClose: 3000,
    hideProgressBar: false,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
  })
}

export const showInfoToast = (message: string) => {
  toast.info(message, {
    position: 'top-right',
    autoClose: 3000,
    hideProgressBar: false,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
  })
}

const toastr = {
  success: (message: string) => toast.success(message),
  error: (message: string) => toast.error(message),
  confirm: (message: string, { onOk }: { onOk: () => void }) => {
    if (window.confirm(message)) {
      onOk()
    }
  },
  warning: (message: string) => toast.warn(message)
}

export default toastr